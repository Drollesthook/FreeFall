using UnityEngine;

public class Plane : MonoBehaviour {

    [SerializeField] int _planeSpeedReducer = default, _planeFinaleSpeedReducer = default;
    [SerializeField] LayerMask _playerMask = default;
    [SerializeField] int _caughtRange = default;
    
    PlaneMovementController _planeMovementController;

    bool _isPlayerNearby;
    Transform _currentPlayer;
    
    void Awake() {
        _planeMovementController = gameObject.GetComponent<PlaneMovementController>();
    }

    void Update() {
        if (_isPlayerNearby) return;
        CheckForPlayer();
    }

    void OnEnable() {
        _isPlayerNearby = false;
        SetPlaneSpeedReducer(_planeSpeedReducer);
    }

    void SetPlaneSpeedReducer(int speedreducer) {
        _planeMovementController.SetSpeedReducer(speedreducer);
    }

    void CheckForPlayer() {
        if (Physics.OverlapSphere(transform.position, _caughtRange, _playerMask).Length == 0) return;
        _isPlayerNearby = true;
        SetPlaneSpeedReducer(_planeFinaleSpeedReducer);
        GameManager.Instance.CatchPlane();
    }
    
    
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _caughtRange);
    }
}
