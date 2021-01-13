using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour {
    [SerializeField] Transform _plane;
    [SerializeField] Image _progressBar;

    Transform _player;
    float _startDistance, _currentDistance;
    bool _isNeedToCount;

    void Start() {
        LevelBuilder.Instance.PlayerSpawned += OnPlayerSpawned;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
    }

    void OnDestroy() {
        LevelBuilder.Instance.PlayerSpawned -= OnPlayerSpawned;
        GameManager.Instance.GameplayStarted -= OnGameplayStarted;
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
    }

    void FixedUpdate() {
        if(_isNeedToCount)
        CountProgression();
    }

    void OnPlayerSpawned(Transform player) {
        _player = player;
    }

    void OnNewLevelStarted() {
        _isNeedToCount = false;
    }

    void OnGameplayStarted() {
        StartCoroutine(CountStartDistanceWithDelay());
    }

    void CountStartDistance() {
        _startDistance = _plane.transform.position.x - _player.transform.position.x;
        _isNeedToCount = true;
    }

    void CountProgression() {
        _currentDistance = _plane.transform.position.x - _player.transform.position.x;
        _progressBar.fillAmount = (_startDistance - _currentDistance) / _startDistance;
    }

    IEnumerator CountStartDistanceWithDelay() {
        yield return new WaitForEndOfFrame();
        CountStartDistance();
    }
}
