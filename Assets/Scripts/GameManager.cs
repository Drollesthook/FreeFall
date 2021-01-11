using System;
using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour {
    public event Action NewLevelStarted, GameplayStarted, GameReseted, PlaneCaughtUp, LevelCompleted, LevelFailed, PlayerCrashed;
    public static GameManager Instance => _instance;
    public int WorldSpeed => _worldSpeed;
    
    [SerializeField] int _worldSpeed = default;

    bool _isLevelEnds;

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
        _isLevelEnds = false;
    }

    public void CatchPlane() {
        PlaneCaughtUp?.Invoke();
    }

    public void LevelCompletion() {
        EndLevelWithDelay();
        LevelCompleted?.Invoke();
    }

    public void LevelFailure() {
        EndLevelWithDelay();
        LevelFailed?.Invoke();
    }

    public void PlayerCrashes() {
        EndLevelWithDelay();
        PlayerCrashed?.Invoke();
    }

    public void StartGameplay() {
        GameplayStarted?.Invoke();
    }

    void EndLevelWithDelay() {
        if(_isLevelEnds) return;
        _isLevelEnds = true;
        StartCoroutine(EndLevel());
    }
    
    IEnumerator EndLevel() {
        yield return new WaitForSeconds(4f);
        ResetGame();
    }
}
