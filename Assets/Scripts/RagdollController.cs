using System.Collections;
using System.Collections.Generic;

using Lean.Pool;

using UnityEngine;

public class RagdollController : MonoBehaviour {
    [SerializeField] Transform _bike = default;
    
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
        CountPartsStartPositions();
        CountPartsStartRotations();
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
    }

    void OnNewLevelStarted() {
        turnOnKinematic();
        turnOffCollisions();
        GetBackToBike();
        ReturnPartsOnPositions();
        ReturnPartsOnRotations();
    }

    void OnPlayerCrashed() {
        GetRidOffBike();
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

    void CountPartsStartPositions() {
        _partsStartPositions = new Vector3[_rigidbodies.Length];
        for (int i = 0; i < _partsStartPositions.Length; i++) {
            _partsStartPositions[i] = _rigidbodies[i].transform.localPosition;
        }
        _startPosition = transform.position;
    }
    
    void CountPartsStartRotations() {
        _partsStartRotations = new Vector3[_rigidbodies.Length];
        for (int i = 0; i < _partsStartRotations.Length; i++) {
            _partsStartRotations[i] = _rigidbodies[i].transform.localEulerAngles;
        }
        _startRotation = transform.eulerAngles;
    }

    void ReturnPartsOnPositions() {
        if (_partsStartPositions == null) return;
        for (int i = 0; i < _partsStartPositions.Length; i++) {
            _rigidbodies[i].transform.localPosition = _partsStartPositions[i];
        }

        transform.position = _startPosition;
    }

    void ReturnPartsOnRotations() {
        if (_partsStartRotations == null) return;
        for (int i = 0; i < _partsStartRotations.Length; i++) {
            _rigidbodies[i].transform.localEulerAngles = _partsStartRotations[i];
        }
        transform.eulerAngles = _startRotation;
    }

    void GetRidOffBike() {
        transform.SetParent(null);
    }

    void GetBackToBike() {
        transform.SetParent(_bike);
    }

    void CreateSomeVelocityOnCrash() {
        foreach (Rigidbody rigidbody in _rigidbodies) {
            rigidbody.velocity = new Vector3(20,0,0);
        }
    }
    
}
