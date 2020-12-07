using System.Collections;
using System.Collections.Generic;

using UnityEditorInternal;

using UnityEngine;

public class JointHandler : MonoBehaviour {
    [SerializeField] LayerMask _planeMask;
    [SerializeField] Transform _grapplePoint;

    GameObject _grappleTarget;
    
    FixedJoint _joint;
    Plane _currentPlane;
    RaycastHit _hit;
    LineRenderer _lineRenderer;
    bool _isGrappled;
    Rigidbody _rb;

    Rigidbody[] _rigidbodies;

    void Awake() {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _rb = GetComponentInParent<Rigidbody>();
    }

    void LateUpdate() {
        if(_isGrappled)
        DrawLineRenderer();
    }

    public void ThrowHook() {
        Physics.Raycast(_grapplePoint.position, Vector3.right, out _hit, 100, _planeMask);
        if (_hit.collider != null) {
            _grappleTarget = _hit.collider.gameObject;
                CreateHook();
                _isGrappled = true;
        }
    }

    void StopHooking() {
        _isGrappled = false;
        _lineRenderer.enabled = false;
    }

    void CreateHook() {
        _joint = _grappleTarget.GetComponent<FixedJoint>();
        if(_joint == null) _joint = _grappleTarget.AddComponent<FixedJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedBody = _rb;
        _joint.massScale = 0.1f;
    }

    void DrawLineRenderer() {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _grapplePoint.position);
        _lineRenderer.SetPosition(1, _grappleTarget.transform.position);
    }

    
}
