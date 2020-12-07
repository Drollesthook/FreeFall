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

    bool _isPlayerMovable;
    float _playerSpeed;
    Rigidbody _playerRb;
    
    Vector3 _playerStartPosition;
    Vector3 _currentSpeed;

    TouchState _currentTouchState;
    RotateState _currentRotateState;
    enum TouchState {
        Left,
        Right,
        Released
    }

    enum RotateState {
        Rotated,
        Straight
    }
    
    void Start() {
        _isPlayerMovable = true;
        _playerStartPosition = transform.position;
        _playerSpeed = GameManager.Instance.WorldSpeed *_speedMultipyer;
        GameManager.Instance.GameReseted += OnGameReseted;
        GameManager.Instance.LevelCompleted += OnLevelEnded;
        GameManager.Instance.LevelFailed += OnLevelEnded;
        _playerRb = GetComponent<Rigidbody>();
    }

    void OnDestroy() {
        GameManager.Instance.GameReseted -= OnGameReseted;
        GameManager.Instance.LevelCompleted -= OnLevelEnded;
        GameManager.Instance.LevelFailed -= OnLevelEnded;
    }

    void FixedUpdate() {
        if(!_isPlayerMovable) return;
        CalculateMoveVelocity();
        TurnClamper();
        MoveForwardRb();
    }

    void Update() {
        if (!_isPlayerMovable) return;
        HandleInput();
        RotateHandler();
    }

    void RotateHandler() {
        if (_currentRotateState == RotateState.Straight) {
            if (_currentTouchState == TouchState.Released) return;
            if (_currentTouchState == TouchState.Left) {
                RotateOnTurn(_XRotationAngle);
                _currentRotateState = RotateState.Rotated;
            }
            if (_currentTouchState == TouchState.Right) {
                RotateOnTurn(-_XRotationAngle);
                _currentRotateState = RotateState.Rotated;
            }
        } else if(_currentTouchState == TouchState.Released) {
            RotateOnTurn(0);
            _currentRotateState = RotateState.Straight;
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

    public void OnLevelEnded() {
        _isPlayerMovable = false;
        _playerSpeed = 0;
    }
    void OnGameReseted() {
        _isPlayerMovable = true;
        _playerSpeed = GameManager.Instance.WorldSpeed *_speedMultipyer;
        transform.position = _playerStartPosition;
        _playerRb.velocity = Vector3.zero;
    }
}
