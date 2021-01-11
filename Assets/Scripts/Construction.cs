using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using Lean.Pool;

using UnityEngine;

public class Construction : MonoBehaviour {
    
    [SerializeField] float _safeDistant = default;
    [SerializeField] float _activatedPosition = default;
    [SerializeField] float _riseTime, _delayTime;
    [SerializeField] LayerMask _playerMask;

    Vector3 _startPosition;
    ConstructionPart[] _constructionParts;

    void Start() {
        _constructionParts = GetComponentsInChildren<ConstructionPart>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plane")) StartCoroutine(ActivateWithDelay());
    }

    void OnDestroy() {
        DOTween.Kill(transform);
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

    void DeactivateKinematicOnChildrens() {
        if (_constructionParts.Length == 0) return;
        foreach (ConstructionPart part in _constructionParts) {
            part.DeactivateKinematic();
        }
    }

    IEnumerator ActivateWithDelay() {
        yield return new WaitForSeconds(_delayTime);
        ActivateBoard();
        yield return new WaitForSeconds(_riseTime);
        DeactivateKinematicOnChildrens();
    }
}
