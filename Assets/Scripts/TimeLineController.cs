using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class TimeLineController : MonoBehaviour {
    [SerializeField] PlayableDirector _winConditionPlayableDirector;


    void Start() {
        GameManager.Instance.PlaneCaughtUp += OnPlaneCaughtUp;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
    }

    void OnDestroy() {
        GameManager.Instance.PlaneCaughtUp -= OnPlaneCaughtUp;
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
    }

    public void OnPlaneCaughtUp() {
        _winConditionPlayableDirector.Play();
    }

    public void OnNewLevelStarted() {
        _winConditionPlayableDirector.Stop();
    }
}
