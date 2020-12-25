using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using Lean.Pool;

using UnityEngine;

public class Construction : MonoBehaviour, IPoolable {
    public bool IsSpawned => _isSpawned;
    
    [SerializeField] bool _isDeadly = default;
    [SerializeField] float _safeDistant = default;
    [SerializeField] float _activatedPosition = default;
    [SerializeField] float _riseTime, _delayTime;
    [SerializeField] LayerMask _playerMask;

    Vector3 _startPosition;
    bool _isSpawned;

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player") && _isDeadly)
            GameManager.Instance.PlayerCrashes();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plane")) StartCoroutine(ActivateWithDelay());
    }
    
    
    void ActivateBoard() {
        if (!IsPlayerFar()) return;
        transform.DOMoveY(_activatedPosition, _riseTime);
    }

    bool IsPlayerFar() {
        return Physics.OverlapSphere(transform.position, _safeDistant, _playerMask).Length == 0;
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _safeDistant);
    }

    IEnumerator ActivateWithDelay() {
        yield return new WaitForSeconds(_delayTime);
        ActivateBoard();
        
    }

    public void OnSpawn() {
        _isSpawned = true;
    }

    public void OnDespawn() {
        _isSpawned = false;
    }
}
