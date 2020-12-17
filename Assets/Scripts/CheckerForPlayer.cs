using UnityEngine;

public class CheckerForPlayer : MonoBehaviour {
    [SerializeField] bool _isThisWinTrigger;

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
        if (_isThisWinTrigger) {
            print("LevelEnded");
            _player = other.gameObject;
            DeactivatePlayer();
            GameManager.Instance.LevelCompletion();
            _isLevelCompleted = true;
        } else {
            GameManager.Instance.LevelFailure();
            _isLevelCompleted = true;
        }
    }

    void DeactivatePlayer() {
        _player.GetComponent<Rigidbody>().isKinematic = false;
        _player.transform.SetParent(transform);
    }
}
