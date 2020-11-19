using System.Collections;
using System.Collections.Generic;

using Lean.Pool;

using UnityEngine;

public class Tile : MonoBehaviour, IPoolable {
    [SerializeField] int _tileLength = default;

    public int TileLength => _tileLength;
    
    SpringBoard[] _listOfSpringBoards;

    void Awake() {
        _listOfSpringBoards = GetComponentsInChildren<SpringBoard>();
    }

    public void OnDespawn() {
        foreach (SpringBoard get in _listOfSpringBoards) {
            get.Deactivate();
        }
    }

    public void OnSpawn() {
    }
}
