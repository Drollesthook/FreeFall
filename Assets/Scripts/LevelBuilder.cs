using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
    [SerializeField] TileSpawnManager _tileSpawnManager = default;
    [SerializeField] List<Tile> _tilesLibrary = default;
    [SerializeField] List<Plane> _planesLibrary = default;
    [SerializeField] Vector3 _planeStartPosition = default;
    [SerializeField] int _planePosPerLevelIncreaser = default;
 
    Vector3 _planeSpawnPosition;
    int _currentLevelNumber;
    Plane _currentPlane;

    void Start() {
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
    }

    void OnNewLevelStarted() {
        GetLevelNumber();
        DespawnPlane();
        _tileSpawnManager.ChangeTile(GetNewTile());
        _tileSpawnManager.Reset();
        GetNewPlane();
        SpawnPlane();
    }

    Tile GetNewTile() {
        int i = Random.Range(0, _tilesLibrary.Count);
        return _tilesLibrary[i];
    }

    void GetNewPlane() {
        int i = Random.Range(0, _planesLibrary.Count);
        _currentPlane = _planesLibrary[i];
    }

    void CountPlanePosition() {
        _planeSpawnPosition = new Vector3(_planeStartPosition.x + _currentLevelNumber * _planePosPerLevelIncreaser, _planeStartPosition.y, _planeStartPosition.z);
    }

    void SpawnPlane() {
        CountPlanePosition();
        _currentPlane.transform.position = _planeSpawnPosition;
        _currentPlane.gameObject.SetActive(true);
    }

    void DespawnPlane() {
        if(_currentPlane != null)
        _currentPlane.gameObject.SetActive(false);
    }

    void GetLevelNumber() {
        _currentLevelNumber = LevelManager.Instance.CurrentLevelNumber;
    }
}
