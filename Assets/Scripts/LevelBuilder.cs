using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LevelBuilder : MonoBehaviour {
    public event Action<Transform> PlayerSpawned;
    public static LevelBuilder Instance => _instance;
    
    [SerializeField] TileSpawnManager _tileSpawnManager = default;
    [SerializeField] List<Tile> _tilesLibrary = default;
    [SerializeField] List<Plane> _planesLibrary = default;
    [SerializeField] Vector3 _planeStartPosition = default, _playerStartPosition = default;
    [SerializeField] int _planePosPerLevelIncreaser = default;
    [SerializeField] GameObject _playerPrefub = default;

    static LevelBuilder _instance;
    Vector3 _planeSpawnPosition;
    int _currentLevelNumber;
    Plane _currentPlane;
    GameObject _currentPlayer;

    void Awake() {
        _instance = this;
    }

    void Start() {
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.GameplayStarted -= OnGameplayStarted;
    }

    void OnNewLevelStarted() {
        GetLevelNumber();
        DespawnPlane();
        _tileSpawnManager.ChangeTile(GetNewTile());
        _tileSpawnManager.Reset();
        GetNewPlane();
        SpawnNewPlayer();
    }

    void SpawnNewPlayer() {
        if (_currentPlayer != null) Destroy(_currentPlayer);
        _currentPlayer = Instantiate(_playerPrefub, _playerStartPosition, Quaternion.identity);
        PlayerSpawned?.Invoke(_currentPlayer.transform);
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
        _planeSpawnPosition = new Vector3(_currentPlayer.transform.position.x + _planeStartPosition.x + _currentLevelNumber * _planePosPerLevelIncreaser, _planeStartPosition.y, _planeStartPosition.z);
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

    void OnGameplayStarted() {
        SpawnPlane();
    }
}
