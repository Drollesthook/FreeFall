using DG.Tweening;

using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    [SerializeField] int _minClampedZPos = default, _maxClampedZPos = default;
    [SerializeField] float _XScreenCenterValue = default;
    [SerializeField] float _yInputThreshold = default;
    [SerializeField] int _turnSpeed = default;
    [SerializeField] float _XRotationAngle = default, _yRotationAngle = default;
    [SerializeField] float _turnRotationTime = default, _rotateToZeroTime;
    [SerializeField] float _speedMultiplier = default;
    [SerializeField] Transform _visual = default;
    [SerializeField] float _safeHallToPlane = default;

    bool _isPlayerMovable, _isPlayerControllable, _isItTimeToCatchPlane;
    float _playerSpeed;
    Rigidbody _playerRb;
    Sequence _turnSequence;

    Vector3 _playerStartPosition;
    Vector3 _currentSpeed;

    TouchState _currentTouchState;
    RotateState _currentRotateState;

    enum TouchState {
        Released,
        Left,
        Right
    }

    enum RotateState {
        RotatedOnLeft,
        RotatedOnRight,
        Straight
    }

    void Start() {
        _isPlayerMovable = true;
        _isPlayerControllable = false;
        _playerStartPosition = transform.position;
        _playerSpeed = GameManager.Instance.WorldSpeed * _speedMultiplier;
        GameManager.Instance.GameReseted += OnGameReseted;
        GameManager.Instance.LevelCompleted += OnLevelEnded;
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
        GameManager.Instance.PlaneCaughtUp += OnPlaneCatched;
        _playerRb = GetComponent<Rigidbody>();
    }

    void OnDestroy() {
        GameManager.Instance.GameReseted -= OnGameReseted;
        GameManager.Instance.LevelCompleted -= OnLevelEnded;
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
        GameManager.Instance.GameplayStarted -= OnGameplayStarted;
        GameManager.Instance.PlaneCaughtUp -= OnPlaneCatched;
    }

    void FixedUpdate() {
        if (!_isPlayerMovable) return;
        CalculateMoveVelocity();
        TurnClamper();
        MoveForwardRb();
        RotateHandler();
        if(!_isItTimeToCatchPlane) return;
        TurnToZero();
    }

    void Update() {
        if (!_isPlayerControllable) return;
        HandleInput();
    }

    void RotateHandler() {
        if (_currentTouchState == TouchState.Released) {
            if (_currentRotateState == RotateState.RotatedOnRight || _currentRotateState == RotateState.RotatedOnLeft) {
                RotateToZero();
                _currentRotateState = RotateState.Straight;
            }
        }

        if (_currentTouchState == TouchState.Left) {
            if (_currentRotateState != RotateState.RotatedOnLeft) {
                RotateOnTurn(_XRotationAngle, _yRotationAngle);
                _currentRotateState = RotateState.RotatedOnLeft;
            }
        }

        if (_currentTouchState == TouchState.Right) {
            if (_currentRotateState != RotateState.RotatedOnRight) {
                RotateOnTurn(-_XRotationAngle, -_yRotationAngle);
                _currentRotateState = RotateState.RotatedOnRight;
            }
        }
    }

    void RotateOnTurn(float xAngle, float yAngle) {
        Vector3 newrotateAngle = new Vector3(xAngle, yAngle, _visual.rotation.z);
        _visual.DORotate(newrotateAngle, _turnRotationTime);
    }

    void RotateToZero() {
        DOTween.Kill(_visual);
        _visual.DORotate(Vector3.zero, _rotateToZeroTime);
    }

    void MoveForwardRb() {
        _playerRb.velocity = _currentSpeed;
    }

    void CalculateMoveVelocity() {
        var currentVelocity = _playerRb.velocity;
        _currentSpeed = new Vector3(_playerSpeed, currentVelocity.y, GetStrafeSpeed());
    }

    float GetStrafeSpeed() {
        if (_currentTouchState == TouchState.Released) {
            return 0;
        }

        return _currentTouchState == TouchState.Right ? -_turnSpeed : _turnSpeed;
    }

    void TurnClamper() {
        var zPos = _playerRb.position.z;
        var canGoLeft = zPos < _maxClampedZPos && _currentTouchState == TouchState.Left;
        var canGoRight = zPos > _minClampedZPos && _currentTouchState == TouchState.Right;
        if (!canGoLeft && !canGoRight) {
            _currentSpeed.z = 0;
        }
    }

    void HandleInput() {
        if (Input.GetMouseButton(0)) {
            var touchPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (touchPos.y > _yInputThreshold) return;
            var isRight = touchPos.x > _XScreenCenterValue;
            _currentTouchState = isRight ? TouchState.Right : TouchState.Left;
        } else {
            _currentTouchState = TouchState.Released;
        }
    }

    void OnLevelEnded() {
        _isPlayerMovable = false;
        _isPlayerControllable = false;
        _playerSpeed = 0;
        RotateToZero();
    }

    void OnPlayerCrashed() {
        _isPlayerMovable = false;
        _isPlayerControllable = false;
        _playerSpeed = 0;
        _playerRb.velocity = Vector3.zero;
        RotateToZero();
    }

    void OnPlaneCatched() {
        _isPlayerControllable = false;
        RotateToZero();
        _isItTimeToCatchPlane = true;
    }

    void OnGameplayStarted() {
        _isPlayerControllable = true;
    }

    void TurnToZero() {
        if (transform.position.z < -_safeHallToPlane)
            _currentTouchState = TouchState.Left;
        else if (transform.position.z > _safeHallToPlane)
            _currentTouchState = TouchState.Right;
        else {
            _currentTouchState = TouchState.Released;
            _isItTimeToCatchPlane = false;
        }
    }
    
    
    void OnGameReseted() {
        transform.SetParent(null);
        _playerRb.isKinematic = false;
        _isPlayerMovable = true;
        SetSpeedMultiplyer(1);
        transform.position = _playerStartPosition;
        transform.eulerAngles = Vector3.zero;
        _playerRb.velocity = Vector3.zero;
    }

    void SetSpeedMultiplyer(float newSpeedMultiplier) {
        _speedMultiplier = newSpeedMultiplier;
        _playerSpeed = GameManager.Instance.WorldSpeed *_speedMultiplier;
    }
}
