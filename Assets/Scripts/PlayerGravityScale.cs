using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityScale : MonoBehaviour {
    [SerializeField] int _playerGravityAdditionalForce = default;
    [SerializeField] float _raycastRadius = default;
    [SerializeField] LayerMask _groundMask = default;

    Rigidbody _rigidbody;

    void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
        if (IsInAir()) {
            CreateAdditionalGravityForce();
        }
    }

    bool IsInAir() {
        return Physics.OverlapSphere(transform.position, _raycastRadius, _groundMask).Length == 0;
    }

    void CreateAdditionalGravityForce() {
        _rigidbody.AddForce(new Vector3(1,-1,0) * _playerGravityAdditionalForce);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _raycastRadius);
    }
}
