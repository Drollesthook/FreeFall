using UnityEngine;

public class CheckerForPlayer : MonoBehaviour {
    GameObject _player;

    bool _isLevelCompleted;
    void Start() {
        _isLevelCompleted = false;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
    }

    void OnNewLevelStarted() {
        _isLevelCompleted = false;
    }

    void OnTriggerEnter(Collider other) {
        if (_isLevelCompleted) return;
        if (!other.CompareTag("Player")) return;
            _isLevelCompleted = true;
            _player = other.gameObject;
            DeactivatePlayer();
            GameManager.Instance.LevelCompletion();
        
    }

    void DeactivatePlayer() {
        _player.GetComponent<Rigidbody>().isKinematic = false;
        _player.transform.SetParent(transform);
    }
}
