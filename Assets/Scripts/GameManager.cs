using System;

using UnityEngine;

public class GameManager : MonoBehaviour {
    public event Action NewLevelStarted, GameReseted, PlaneCaughtUp;
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

}
