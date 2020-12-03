using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {

    [SerializeField] int _planeSpeedReducer = default;
    [SerializeField] LayerMask _playerMask = default;
    [SerializeField] int _caughtRange = default;
    
    PlaneMovementController _planeMovementController;
    ObstacleSpawner[] _listOfObstacleSpawners;

    bool _isPlayerNearby;
    
    void Awake() {
        _planeMovementController = gameObject.GetComponent<PlaneMovementController>();
        _listOfObstacleSpawners = GetComponentsInChildren<ObstacleSpawner>();
    }

    void Update() {
        if (_isPlayerNearby) return;
        CheckForPlayer();
    }

    void OnEnable() {
        StartObstacleSpawn();
        _isPlayerNearby = false;
        SetPlaneSpeedReducer(_planeSpeedReducer);
    }

    void OnDisable() {
        StopObstacleSpawn();
    }

    void SetPlaneSpeedReducer(int speedreducer) {
        _planeMovementController.SetSpeedReducer(speedreducer);
    }
    

    void StartObstacleSpawn() {
        foreach (ObstacleSpawner spawner in _listOfObstacleSpawners) {
            spawner.StartSpawn();
        }
    }
    
    void StopObstacleSpawn() {
        foreach (ObstacleSpawner spawner in _listOfObstacleSpawners) {
            spawner.StopSpawn();
        }
    }

    void CheckForPlayer() {
        if (Physics.OverlapSphere(transform.position, _caughtRange, _playerMask).Length == 0) return;
        _isPlayerNearby = true;
        StopObstacleSpawn();
        SetPlaneSpeedReducer(0);
    }
    
    
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _caughtRange);
    }
}
