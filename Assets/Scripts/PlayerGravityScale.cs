using DG.Tweening;

using UnityEngine;

public class PlayerGravityScale : MonoBehaviour {
    [SerializeField] int _playerGravityAdditionalForce = default, _playerPlaneAdditionalForce = default, _tramplinAdditionalForce = default;
    [SerializeField] float _raycastInAirRange = default;
    [SerializeField] LayerMask _groundMask = default;
    [SerializeField] Transform _visual = default;
    [SerializeField] float _rotationTime = default;
    [SerializeField] Transform _raycastPoint = default;

    Rigidbody _rigidbody;
    RaycastHit _hit;
    float _currentAngle;
    int _playerUsualForce;

    void Start() {
        _currentAngle = 0;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        GameManager.Instance.PlaneCaughtUp += OnPlaneCaught;
    }

    void OnDestroy() {
        GameManager.Instance.PlaneCaughtUp -= OnPlaneCaught;
    }

    void Update() {
        if (IsInAir()) {
            CreateAdditionalGravityForce(_playerGravityAdditionalForce);
            RotateBike(0);
        } else {
            CreateAdditionalGravityForce(_playerUsualForce);
            RotateBike(_hit.transform.eulerAngles.z);
        }
    }

    void OnPlaneCaught() {
        _playerUsualForce = _playerPlaneAdditionalForce;
    }

    bool IsInAir() {
        return !Physics.Raycast(_raycastPoint.position, new Vector3(0,-1,0), out _hit, _raycastInAirRange, _groundMask);
    }

    void CreateAdditionalGravityForce(int force) {
        _rigidbody.AddForce(new Vector3(1,-1,0) * force);
    }

    void RotateBike(float angle) {
        if (angle != 0)
            _rigidbody.AddRelativeForce(Vector3.right * _tramplinAdditionalForce);
        if (angle == _currentAngle)  return;
        _currentAngle = angle;
        Vector3 newrotateAngle= new Vector3(transform.rotation.x, transform.rotation.y,angle);
        _visual.DORotate(newrotateAngle, _rotationTime);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(_raycastPoint.position, new Vector3(0,-1,0));
    }
}
