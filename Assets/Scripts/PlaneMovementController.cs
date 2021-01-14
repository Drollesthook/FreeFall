using DG.Tweening;

using UnityEngine;

public class PlaneMovementController : MonoBehaviour {
    [SerializeField] Vector3 _flyAwayRotation;
    [SerializeField] Transform _hutch;
    [SerializeField] Vector3 _openedHutchRotation, _closedHutchRotation;
    int _planeSpeed;
    Vector3 _planeStartPosition;
    Vector3 _planeMoveDirection;

    void Awake() {
        _planeMoveDirection = Vector3.right;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.LevelCompleted += OnLevelEnded;
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.LevelCompleted -= OnLevelEnded;
    }

    void Update() {
        MoveForward();
    }
    
    void MoveForward() {
        Vector3 newMoveDirection = _planeMoveDirection * _planeSpeed * Time.deltaTime;
        var newPosition = transform.position + newMoveDirection;
        transform.position = newPosition;
    }

    public void SetSpeedReducer(int speedReducer) {
        _planeSpeed = GameManager.Instance.WorldSpeed - speedReducer;
    }

    void OnNewLevelStarted() {
        transform.eulerAngles = Vector3.zero;
        _planeMoveDirection = Vector3.right;
        _hutch.DOLocalRotate(_openedHutchRotation, 1);
    }

    void OnLevelEnded() {
        CloseTheHutch();
        FlyAway();
        _planeMoveDirection = new Vector3(1, 0.2f, 0);
    }

    void CloseTheHutch() {
        _hutch.DOLocalRotate(_closedHutchRotation, 1);
    }

    void FlyAway() {
        transform.DORotate(_flyAwayRotation, 3);
    }
}
