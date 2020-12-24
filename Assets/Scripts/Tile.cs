using System.Collections;
using System.Collections.Generic;

using Lean.Pool;

using UnityEngine;

public class Tile : MonoBehaviour, IPoolable {
    ConstructionSpawnPoint[] _listOfConstructionSpawnPoints;

    int _currentLevelNumber;

    void Awake() {
        _listOfConstructionSpawnPoints = GetComponentsInChildren<ConstructionSpawnPoint>();
    }

    public void OnDespawn() {
        foreach (ConstructionSpawnPoint point in _listOfConstructionSpawnPoints) {
            point.Deactivate();
        }
    }

    public void OnSpawn() {
        GetLevelNumber();
        GetConstructions();
    }

    void GetLevelNumber() {
        _currentLevelNumber = LevelManager.Instance.CurrentLevelNumber;
    }

    void GetConstructions() {
        foreach (ConstructionSpawnPoint point in _listOfConstructionSpawnPoints) {
            point.SpawnConstruction(_currentLevelNumber);
        }
    }
}
