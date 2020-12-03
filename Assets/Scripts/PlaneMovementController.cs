using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementController : MonoBehaviour {
    int _planeSpeed;
    Vector3 _planeStartPosition;

    void Update() {
        MoveForward();
    }
    
    void MoveForward() {
        Vector3 newMoveDirection = Vector3.right * _planeSpeed * Time.deltaTime;
        var newPosition = transform.position + newMoveDirection;
        transform.position = newPosition;
    }

    public void SetSpeedReducer(int speedReducer) {
        _planeSpeed = GameManager.Instance.WorldSpeed - speedReducer;
    }
}
