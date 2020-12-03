using System.Collections.Generic;

using Lean.Pool;

using UnityEngine;

public class TileSpawnManager : MonoBehaviour {
    [SerializeField] Tile _tile = default;
    [SerializeField] Transform _player = default;
    [SerializeField] int _tileLength = default;
    [SerializeField] int _renderDistance = 1;
    
    float _playerXPos, _spawnOffset, _overallLength;
    int _numberOfSpawnedTiles;
    Queue<Tile> _tilesQueue = new Queue<Tile>();
    void Start() {
        _spawnOffset = _tileLength * _renderDistance;
        GameManager.Instance.GameReseted += OnGameReseted;
    }

    void OnDestroy() {
        GameManager.Instance.GameReseted -= OnGameReseted;
    }

    void Update() {
        CheckForNeedToSpawn();
    }

    public void ChangeTile(Tile newTile) {
        _tile = newTile;
    }
    public void Reset() {
        LeanPool.DespawnAll();
        _numberOfSpawnedTiles = 0;
        _overallLength = 0;
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
    
    

    Vector3 CountBlockSpawnPos() {
        return new Vector3(_numberOfSpawnedTiles * _tileLength, 0, 0);
    }

    void OnGameReseted() {
        Reset();
    }
}
