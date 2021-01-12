using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public event Action<int> LevelCompleted;
    public static LevelManager Instance => _instance;
    public int CurrentLevelNumber => _currentLevelNumber;

    const string CURRENT_LEVEL = "current_level";
    
    int _currentLevelNumber;
    static LevelManager _instance;

    void Awake() {
        _instance = this;
        _currentLevelNumber = PlayerPrefs.GetInt(CURRENT_LEVEL, 1);
    }

    void Start() {
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
    }

    void OnDestroy() {
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
    }

    void OnLevelCompleted() {
        _currentLevelNumber++;
        PlayerPrefs.SetInt(CURRENT_LEVEL, _currentLevelNumber);
        LevelCompleted?.Invoke(_currentLevelNumber);
    }
}
