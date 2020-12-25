using DG.Tweening;

using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    [SerializeField] int _minClampedZPos = default, _maxClampedZPos = default;
    [SerializeField] float _XScreenCenterValue = default;
    [SerializeField] float _yInputThreshold = default;
    [SerializeField] int _turnSpeed = default;
    [SerializeField] float _XRotationAngle = default;
    [SerializeField] float _turnRotationTime = default;
    [SerializeField] float _speedMultipyer = default;
    [SerializeField] Transform _visual = default;

    bool _isPlayerMovable, _isPlayerControllable;
    float _playerSpeed;
    Rigidbody _playerRb;
    
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
        _playerSpeed = GameManager.Instance.WorldSpeed *_speedMultipyer;
        GameManager.Instance.GameReseted += OnGameReseted;
        GameManager.Instance.LevelCompleted += OnLevelEnded;
        GameManager.Instance.LevelFailed += OnLevelEnded;
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
        _playerRb = GetComponent<Rigidbody>();
    }

    void OnDestroy() {
        GameManager.Instance.GameReseted -= OnGameReseted;
        GameManager.Instance.LevelCompleted -= OnLevelEnded;
        GameManager.Instance.LevelFailed -= OnLevelEnded;
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
        GameManager.Instance.GameplayStarted -= OnGameplayStarted;
    }

    void FixedUpdate() {
        if(!_isPlayerMovable) return;
        CalculateMoveVelocity();
        TurnClamper();
        MoveForwardRb();
    }

    void Update() {
        if (!_isPlayerControllable) return;
        HandleInput();
        RotateHandler();
    }

    void RotateHandler() {
        if (_currentTouchState == TouchState.Released) {
            if (_currentRotateState == RotateState.RotatedOnRight || _currentRotateState == RotateState.RotatedOnLeft) {
                RotateOnTurn(0);
                _currentRotateState = RotateState.Straight;
            }
        }

        if (_currentTouchState == TouchState.Left) {
            if (_currentRotateState != RotateState.RotatedOnLeft) {
                RotateOnTurn(_XRotationAngle);
                _currentRotateState = RotateState.RotatedOnLeft;
            }    
        }
        
        if (_currentTouchState == TouchState.Right) {
            if (_currentRotateState != RotateState.RotatedOnRight) {
                RotateOnTurn(-_XRotationAngle);
                _currentRotateState = RotateState.RotatedOnRight;
            }    
        }
    }

    void RotateOnTurn(float xAngle) {
        Vector3 newrotateAngle= new Vector3(xAngle,0,0);
        _visual.DORotate(newrotateAngle, _turnRotationTime);
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
        RotateOnTurn(0);
    }

    void OnPlayerCrashed() {
        _isPlayerMovable = false;
        _isPlayerControllable = false;
        _playerSpeed = 0;
        _playerRb.velocity = new Vector3(1,0,0);
        RotateOnTurn(0);
    }

    void OnGameplayStarted() {
        _isPlayerControllable = true;
    }
    
    
    void OnGameReseted() {
        transform.SetParent(null);
        _playerRb.isKinematic = false;
        _isPlayerMovable = true;
        _playerSpeed = GameManager.Instance.WorldSpeed *_speedMultipyer;
        transform.position = _playerStartPosition;
        transform.eulerAngles = Vector3.zero;
        _playerRb.velocity = Vector3.zero;
    }
}
