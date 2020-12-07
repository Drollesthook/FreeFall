using UnityEngine;

public class CheckerForPlayer : MonoBehaviour {
    [SerializeField] bool _isThisWinTrigger;
    [SerializeField] FixedJoint _fixedJoint;

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
        if(_fixedJoint.connectedBody != null)
        _fixedJoint.connectedBody = null;
    }

    void OnTriggerEnter(Collider other) {
        if (_isLevelCompleted) return;
        if (!other.CompareTag("Player")) return;
        if (_isThisWinTrigger) {
            _fixedJoint.connectedBody = other.GetComponent<Rigidbody>();
            GameManager.Instance.LevelCompletion();
            _isLevelCompleted = true;
        } else {
            GameManager.Instance.LevelFailure();
            _isLevelCompleted = true;
        }
    }
}
