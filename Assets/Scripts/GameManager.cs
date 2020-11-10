using System;

using UnityEngine;

public class GameManager : MonoBehaviour {
    public event Action GameReseted;
    public static GameManager Instance => _instance;
    public int WorldSpeed => _worldSpeed;
    
    [SerializeField] int _worldSpeed = default;

    static GameManager _instance;
    void Awake() {
        _instance = this;
    }

    public void ResetGame() {
        GameReseted?.Invoke();
    }

}
