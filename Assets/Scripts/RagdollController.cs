using UnityEngine;

public class RagdollController : MonoBehaviour {
    Rigidbody[] _rigidbodies;
    CapsuleCollider[] _capsuleColliders;
    BoxCollider[] _boxColliders;
    Vector3[] _partsStartRotations;
    Vector3[] _partsStartPositions;
    Vector3 _startPosition, _startRotation;
    
    void Start() {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _capsuleColliders = GetComponentsInChildren<CapsuleCollider>();
        _boxColliders = GetComponentsInChildren<BoxCollider>();
        turnOnKinematic();
        turnOffCollisions();
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
    }

    void OnDestroy() {
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
    }

    void OnPlayerCrashed() {
        turnOffKinematic();
        turnOnCollisions();
        CreateSomeVelocityOnCrash();
    }

    void turnOnKinematic() {
        foreach (Rigidbody rigidbody in _rigidbodies) {
            rigidbody.isKinematic = true;
        }
    }
    
    void turnOffKinematic() {
        foreach (Rigidbody rigidbody in _rigidbodies) {
            rigidbody.isKinematic = false;
        }
    }

    void turnOffCollisions() {
        foreach (CapsuleCollider VARIABLE in _capsuleColliders) {
            VARIABLE.isTrigger = true;
        }
        foreach (BoxCollider VARIABLE in _boxColliders) {
            VARIABLE.isTrigger = true;
        }
    }

    void turnOnCollisions() {
        foreach (CapsuleCollider VARIABLE in _capsuleColliders) {
            VARIABLE.isTrigger = false;
        }
        foreach (BoxCollider VARIABLE in _boxColliders) {
            VARIABLE.isTrigger = false;
        }
    }

    void CreateSomeVelocityOnCrash() {
        foreach (Rigidbody rigidbody in _rigidbodies) {
            rigidbody.velocity = new Vector3(20,0,0);
        }
    }
    
}
