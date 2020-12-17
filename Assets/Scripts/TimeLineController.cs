using System.Collections;

using UnityEngine.Playables;
using UnityEngine;

public class TimeLineController : MonoBehaviour {
    [SerializeField] PlayableDirector _winConditionPlayableDirector, _winCameraDirector, _failCameraDirector, _crashCameraDirector;


    void Start() {
        GameManager.Instance.PlaneCaughtUp += OnPlaneCaughtUp;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        GameManager.Instance.LevelFailed += OnLevelFailed;
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
    }

    void OnDestroy() {
        GameManager.Instance.PlaneCaughtUp -= OnPlaneCaughtUp;
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
        GameManager.Instance.LevelFailed -= OnLevelFailed;
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
    }

    void OnPlaneCaughtUp() {
        _winConditionPlayableDirector.Play();
    }

    void OnNewLevelStarted() {
        _winConditionPlayableDirector.Stop();
        _winCameraDirector.Stop();
        _failCameraDirector.Stop();
        _crashCameraDirector.Stop();
    }

    void OnLevelCompleted() {
        _winConditionPlayableDirector.Stop();
        _winCameraDirector.Play();
    }

    void OnLevelFailed() {
        _winConditionPlayableDirector.Stop();
        _failCameraDirector.Play();
    }

    void OnPlayerCrashed() {
        _crashCameraDirector.Play();
    }

}
