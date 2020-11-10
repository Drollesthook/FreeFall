using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour {
    public static Pooler Instance => _instance;
    
    [SerializeField] Tile _tile = default;
    [SerializeField] int _amountTilesToPrespawn = default;
    
    static Pooler _instance;
    List<Tile> _freeTiles = new List<Tile>();
    //List<Tile> _busyTiles = new List<Tile>();
    
    Queue<Tile> _tilesQueue = new Queue<Tile>();

    void Awake() {
        _instance = this;
        PrespawnTiles();
    }


    public Tile SpawnTiles(Vector3 spawnPosition) {
        while (true) {
            if (_freeTiles.Count > 0) {
                var tilesToSpawn = _freeTiles[0];
                tilesToSpawn.gameObject.SetActive(true);
                _tilesQueue.Enqueue(tilesToSpawn);
                _freeTiles.Remove(tilesToSpawn);
                tilesToSpawn.transform.position = spawnPosition;
                return tilesToSpawn;
            }
            CreateNewTile();
        }
    }

    public void DespawnLastTile() {
        Tile tile = _tilesQueue.Dequeue();
        tile.gameObject.SetActive(false);
        _freeTiles.Add(tile);
    }

    /*public void DespawnTiles() {
        if (_busyTiles.Count <= 0) return;
        foreach (Tile tile in _busyTiles) {
            tile.gameObject.SetActive(false);
            _freeTiles.Add(tile);
        }
        _busyTiles.Clear();
    }*/
    
    
    void PrespawnTiles() {
        for (int i = 0; i < _amountTilesToPrespawn; i++) {
            CreateNewTile();
        }
    }

    void CreateNewTile() {
        var newTile = Instantiate(_tile, transform);
        newTile.gameObject.SetActive(false);
        _freeTiles.Add(newTile);
    }
}
