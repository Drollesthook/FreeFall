using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] Image _winScreen, _loseScreen;
    [SerializeField] GameObject _gameplayScreen;
    
    Color _endColor = new Color(255, 255, 255, 255);
    void Start() {
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        GameManager.Instance.LevelFailed += OnLevelFailed;
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
        GameManager.Instance.LevelFailed -= OnLevelFailed;
    }

    void OnNewLevelStarted() {
        DeactivateLevelEndedScreens();
        _gameplayScreen.SetActive(true);
    }

    void OnLevelFailed() {
        _gameplayScreen.SetActive(false);
        _loseScreen.DOColor(_endColor, 2f);
    }

    void OnLevelCompleted() {
        _gameplayScreen.SetActive(false);
        _winScreen.DOColor(_endColor, 2f);
    }

    void DeactivateLevelEndedScreens() {
        _winScreen.color = new Color(255,255,255,0);
        _loseScreen.color = new Color(255,255,255,0);
    }
}
