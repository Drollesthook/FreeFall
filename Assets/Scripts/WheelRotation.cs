using UnityEngine;

public class WheelRotation : MonoBehaviour {

    [SerializeField] Transform _frontWheel = default, _backWheel = default;
    [SerializeField] float _rotationSpeedMultiplyer = default;
    Rigidbody _playerRb;

    void Awake() {
        _playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        WheelRotating();
    }

    void WheelRotating() {
        _frontWheel.Rotate(Vector3.forward * _playerRb.velocity.magnitude * _rotationSpeedMultiplyer * Time.deltaTime,Space.Self);
        _backWheel.Rotate(Vector3.forward * _playerRb.velocity.magnitude * _rotationSpeedMultiplyer * Time.deltaTime, Space.Self);
    }
}
