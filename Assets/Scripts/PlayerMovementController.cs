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
    float _playerSpeed = default;
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
        _playerStartPosition = transform.position;
        _playerSpeed = GameManager.Instance.WorldSpeed *_speedMultipyer;
        GameManager.Instance.GameReseted += OnGameReseted;
        _playerRb = GetComponent<Rigidbody>();
    }

    void OnDestroy() {
        GameManager.Instance.GameReseted -= OnGameReseted;
    }

    void FixedUpdate() {
        CalculateMoveVelocity();
        TurnClamper();
        MoveForwardRb();
    }

    void Update() {
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
        transform.DORotate(newrotateAngle, _turnRotationTime);
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
    void OnGameReseted() {
        transform.position = _playerStartPosition;
        _playerRb.velocity = Vector3.zero;
    }
}
