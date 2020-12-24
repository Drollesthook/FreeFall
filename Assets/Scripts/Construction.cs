using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using Lean.Pool;

using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] float _safeDistant = default;
    [SerializeField] float _activatedPosition = default;
    [SerializeField] float _riseTime;
    [SerializeField] LayerMask _playerMask;

    Vector3 _startPosition;
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plane") && IsPlayerFar()) ActivateBoard();
    }
    
    
    void ActivateBoard() {
        transform.DOMoveY(_activatedPosition, _riseTime);
    }

    bool IsPlayerFar() {
        return Physics.OverlapSphere(transform.position, _safeDistant, _playerMask).Length == 0;
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _safeDistant);
    }
}
