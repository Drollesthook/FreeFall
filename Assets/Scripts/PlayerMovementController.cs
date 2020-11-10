using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    [SerializeField] int _minClampedZPos = default, _maxClampedZPos = default;
    [SerializeField] float _screenCenterValue = default;
    [SerializeField] int _turnSpeed = default;
    int _playerSpeed = default;
    
    float _touchPosition;
    bool _isScreenTouched = false;
    void Start() {
        _playerSpeed = GameManager.Instance.WorldSpeed;
        InputManager.Instance.ScreenIsTouched += OnScreenTouched;
        InputManager.Instance.ScreenIsReleased += OnScreenReleased;
    }

    void OnDestroy() {
        InputManager.Instance.ScreenIsTouched -= OnScreenTouched;
        InputManager.Instance.ScreenIsReleased -= OnScreenReleased;
    }

    void Update() {
        MoveForward();
        if (!_isScreenTouched) return;
        Turn();
    }

    void MoveForward() {
        Vector3 newMoveDirection = Vector3.right * _playerSpeed * Time.deltaTime;
        var newPosition = transform.position + newMoveDirection;
        transform.position = newPosition;
    }

    void Turn() {
        Vector3 TurnDirection = new Vector3();
        if (_touchPosition >= _screenCenterValue) {
            TurnDirection = Vector3.back * _turnSpeed * Time.deltaTime;
        }
        else if (_touchPosition < _screenCenterValue) {
            TurnDirection = Vector3.forward * _turnSpeed * Time.deltaTime;
        }
        else {
            TurnDirection = Vector3.zero;
        }

        var newPosition = transform.position + TurnDirection;
        var clampedPosition = new Vector3(newPosition.x, newPosition.y,
                                          Mathf.Clamp(newPosition.z, _minClampedZPos, _maxClampedZPos));

        transform.position = clampedPosition;

    }

    void OnScreenTouched(float touchPos) {
        _touchPosition = touchPos;
        _isScreenTouched = true;
    }

    void OnScreenReleased() {
        _isScreenTouched = false;
    }
}
