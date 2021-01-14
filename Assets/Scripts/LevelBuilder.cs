using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LevelBuilder : MonoBehaviour {
    public event Action<Transform> PlayerSpawned;
    public static LevelBuilder Instance => _instance;
    
    public Transform CurrentPlayer => _currentPlayer.transform;

    [SerializeField] bool _isDebug = default;
    [SerializeField] Tile _debugTile = default;
    [SerializeField] TileSpawnManager _tileSpawnManager = default;
    [SerializeField] List<Tile> _tilesLibrary = default;
    [SerializeField] Plane _currentPlane = default;
    [SerializeField] Vector3 _planeStartPosition = default, _playerStartPosition = default;
    [SerializeField] int _planePosPerLevelIncreaser = default;
    [SerializeField] GameObject _playerPrefub = default;
    [SerializeField] List<int> _listOfLastLevelsOnTiles = default;
    [SerializeField] LayerMask _constructionsLayerMask = default;
    [SerializeField] int _safeDistanceForStart = default;

    static LevelBuilder _instance;
    Vector3 _planeSpawnPosition;
    int _currentLevelNumber, _lastFixedLevel;
    GameObject _currentPlayer;
    Collider[] _constructionsToPrespawn;

    void Awake() {
        _instance = this;
    }

    void Start() {
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
        _lastFixedLevel = _listOfLastLevelsOnTiles[_listOfLastLevelsOnTiles.Count - 1];
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
        SpawnNewPlayer();
    }

    void SpawnNewPlayer() {
        if (_currentPlayer != null) Destroy(_currentPlayer);
        _currentPlayer = Instantiate(_playerPrefub, _playerStartPosition, Quaternion.identity);
        PlayerSpawned?.Invoke(_currentPlayer.transform);
    }

    Tile GetNewTile() {
        if (_isDebug) return _debugTile;
        int i = 0;
        if (_currentLevelNumber <= _lastFixedLevel) {
            while (_currentLevelNumber > _listOfLastLevelsOnTiles[i]) {
                i++;
            }
        }
        else i = Random.Range(0, _tilesLibrary.Count);

        return _tilesLibrary[i];
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
        GetConstructionsToPrespawn();
        PrespawnConstructions();
    }

    void GetConstructionsToPrespawn() {
        float playerXPos = _currentPlayer.transform.position.x + _safeDistanceForStart,
              planeXPos = _currentPlane.transform.position.x;

        float overlapRadius = (planeXPos - playerXPos) * 0.5f,
              overlapCenter = playerXPos + overlapRadius;
        _constructionsToPrespawn = Physics.OverlapSphere(new Vector3(overlapCenter,0,0), overlapRadius, _constructionsLayerMask);
    }

    void PrespawnConstructions() {
        foreach (var construction in _constructionsToPrespawn) {
            construction.GetComponent<Construction>().StartCoroutine("ActivateWithDelay");
        }
    }
}
