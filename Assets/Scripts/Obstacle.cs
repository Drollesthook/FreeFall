using System.Collections;
using System.Collections.Generic;

using Lean.Pool;

using UnityEngine;

public class Obstacle : MonoBehaviour, IPoolable {
    Rigidbody _rigidbody;
    void Awake() {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void OnSpawn() {
        
    }

    public void OnDespawn() {
        _rigidbody.velocity = Vector3.zero;
    }
}
