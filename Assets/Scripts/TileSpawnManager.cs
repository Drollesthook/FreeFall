using System.Collections;
using System.Collections.Generic;

using Lean.Pool;

using UnityEngine;

public class TileSpawnManager : MonoBehaviour {
    [SerializeField] Tile _tile = default;
    [SerializeField] Transform _player = default;
    [SerializeField] int _tileLength = default;
    [SerializeField] int _renderDistance = 1;
    
    float _playerXPos, _spawnOffset, _deSpawnOffset, _overallLength;
    int _numberOfSpawnedTiles;
    Queue<Tile> _tilesQueue = new Queue<Tile>();
    void Start() {
        _spawnOffset = _tileLength * _renderDistance;
        _deSpawnOffset = _tileLength;
        StartCoroutine(CheckForDespawnTimer());
    }

    void Update() {
        CheckForNeedToSpawn();
    }
    
    void CheckForNeedToSpawn() {
        if(_overallLength - _spawnOffset > _player.position.x)
            return;
        SpawnNextBlock();
    }
    
    void SpawnNextBlock() {
        _tilesQueue.Enqueue(LeanPool.Spawn(_tile, CountBlockSpawnPos(), Quaternion.identity));
        _numberOfSpawnedTiles++;
        _overallLength = _numberOfSpawnedTiles * _tileLength;
    }

    void CheckForNeedToDespawn() {
        if(_tilesQueue == null)
            return;
        if (_tilesQueue.Peek().transform.position.x + _deSpawnOffset < _player.position.x) {
            DespawnBlock();
        }
    }
    
    void Reset() {
        for(int i = 0; i <= _tilesQueue.Count; i++) {
            LeanPool.Despawn(_tilesQueue.Dequeue());
        }
        _numberOfSpawnedTiles = 0;
        _overallLength = 0;
    }
    
    void DespawnBlock() {
        LeanPool.Despawn(_tilesQueue.Dequeue());
    }

    Vector3 CountBlockSpawnPos() {
        return new Vector3(_numberOfSpawnedTiles * _tileLength, 0, 0);
    }

    IEnumerator CheckForDespawnTimer() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            CheckForNeedToDespawn();
        }
    }
}
