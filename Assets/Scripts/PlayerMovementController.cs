using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    [SerializeField] int _minClampedZPos = default, _maxClampedZPos = default;
    [SerializeField] float _screenCenterValue = default;
    [SerializeField] int _turnSpeed = default;
    int _playerSpeed = default;
    Rigidbody _playerRb;
    
    Vector3 _playerStartPosition;
    Vector3 _currentSpeed;

    TouchState _currentTouchState;
    enum TouchState {
        Left,
        Right,
        Released
    }
    
    void Start() {
        _playerStartPosition = transform.position;
        _playerSpeed = GameManager.Instance.WorldSpeed;
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
            var isRight = touchPos.x > _screenCenterValue;
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
