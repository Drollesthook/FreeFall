using System.Collections;
using System.Collections.Generic;

using Lean.Pool;

using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField] float _periodToSpawnObstacles = default;
    [SerializeField] GameObject _objectToSpawn = default;


    bool _isSpawnGoin;
    public void StartSpawn() {
        _isSpawnGoin = true;
        StartCoroutine(SpawnObstacles());
        print("Obstacles spawn start");
    }

    public void StopSpawn() {
        _isSpawnGoin = false;
        StopCoroutine(SpawnObstacles());
        print("Obstacles spawn stops");
    }

    IEnumerator SpawnObstacles() {
        while (_isSpawnGoin) {
            LeanPool.Spawn(_objectToSpawn, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_periodToSpawnObstacles);
        }
    }
}
