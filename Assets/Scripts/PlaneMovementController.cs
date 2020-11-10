using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementController : MonoBehaviour {
    int _planeSpeed;
    Vector3 _planeStartPosition;

    void Start() {
        _planeStartPosition = transform.position;
        _planeSpeed = GameManager.Instance.WorldSpeed - 5;
        GameManager.Instance.GameReseted += OnGameReseted;
    }

    void OnDestroy() {
        GameManager.Instance.GameReseted -= OnGameReseted;
    }
    void Update() {
        MoveForward();
    }
    
    void MoveForward() {
        Vector3 newMoveDirection = Vector3.right * _planeSpeed * Time.deltaTime;
        var newPosition = transform.position + newMoveDirection;
        transform.position = newPosition;
    }

    void OnGameReseted() {
        transform.position = _planeStartPosition;
    }
}
