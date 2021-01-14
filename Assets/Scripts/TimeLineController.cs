using UnityEngine.Playables;
using UnityEngine;

public class TimeLineController : MonoBehaviour {
    [SerializeField] PlayableDirector _winConditionPlayableDirector, _winCameraDirector, _GameplayStartCameraDirector;


    void Start() {
        GameManager.Instance.PlaneCaughtUp += OnPlaneCaughtUp;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        GameManager.Instance.GameplayStarted += OnGamePlayStarted;
    }

    void OnDestroy() {
        GameManager.Instance.PlaneCaughtUp -= OnPlaneCaughtUp;
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
        GameManager.Instance.GameplayStarted -= OnGamePlayStarted;
    }

    void OnPlaneCaughtUp() {
        _winConditionPlayableDirector.Play();
    }

    void OnNewLevelStarted() {
        _winConditionPlayableDirector.Stop();
        _winCameraDirector.Stop();
        _GameplayStartCameraDirector.Stop();
    }

    void OnLevelCompleted() {
        _winConditionPlayableDirector.Stop();
        _winCameraDirector.Play();
    }

    void OnGamePlayStarted() {
        _GameplayStartCameraDirector.Play();
    }

}
