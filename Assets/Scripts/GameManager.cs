using System;
using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour {
    public event Action NewLevelStarted, GameplayStarted, GameReseted, PlaneCaughtUp, LevelCompleted, LevelFailed, PlayerCrashed;
    public static GameManager Instance => _instance;
    public int WorldSpeed => _worldSpeed;
    
    [SerializeField] int _worldSpeed = default;

    static GameManager _instance;
    void Awake() {
        _instance = this;
    }

    void Start() {
        NewLevelStarted?.Invoke();
    }

    public void ResetGame() {
        GameReseted?.Invoke();
        NewLevelStarted?.Invoke();
    }

    public void CatchPlane() {
        PlaneCaughtUp?.Invoke();
    }

    public void LevelCompletion() {
        StartCoroutine(EndLevel());
        LevelCompleted?.Invoke();
    }

    public void LevelFailure() {
        StartCoroutine(EndLevel());
        LevelFailed?.Invoke();
    }

    public void PlayerCrashes() {
        PlayerCrashed?.Invoke();
    }

    public void StartGameplay() {
        GameplayStarted?.Invoke();
    }
    IEnumerator EndLevel() {
        yield return new WaitForSeconds(4f);
        ResetGame();
    }
}
