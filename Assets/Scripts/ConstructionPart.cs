﻿using UnityEngine;

public class ConstructionPart : MonoBehaviour
{
    [SerializeField] bool _isDeadly = default, _isBreakable = default;
    Rigidbody[] _rigidbodies;
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player") && _isDeadly)
            GameManager.Instance.PlayerCrashes();
    }


    public void DeactivateKinematic() {
        if(!_isBreakable) return;
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _rigidbodies) {
            rb.isKinematic = false;
        }
    }
    
}
