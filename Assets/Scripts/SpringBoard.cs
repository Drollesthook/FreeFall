using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class SpringBoard : MonoBehaviour {
    [SerializeField] Vector3 _activatedRotation = default;
    [SerializeField] float _safeDistant = default;
    [SerializeField] float RotationSpeed = default;
    [SerializeField] LayerMask _playerMask = default;
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plane") && IsPlayerFar()) ActivateBoard();
    }

    void ActivateBoard() {
        transform.DORotate(_activatedRotation, RotationSpeed);
    }

    bool IsPlayerFar() {
        return Physics.OverlapSphere(transform.position, _safeDistant, _playerMask).Length == 0;
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _safeDistant);
    }
    
    public void Deactivate() {
        transform.eulerAngles = Vector3.zero;
        //transform.Rotate(Vector3.zero, Space.Self);
    }
}
