using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] Image _winScreen, _loseScreen;
    [SerializeField] GameObject _gameplayScreen, _mainMenuScreen, _crashScreen;
    
    Color _endColor = new Color(255, 255, 255, 255);
    void Start() {
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        GameManager.Instance.LevelFailed += OnLevelFailed;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
        GameManager.Instance.LevelFailed -= OnLevelFailed;
        GameManager.Instance.GameplayStarted -= OnGameplayStarted;
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
    }

    void OnNewLevelStarted() {
        DeactivateLevelEndedScreens();
        _gameplayScreen.SetActive(false);
        _mainMenuScreen.SetActive(true);
    }

    void OnGameplayStarted() {
        _mainMenuScreen.SetActive(false);
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

    void OnPlayerCrashed() {
        _gameplayScreen.SetActive(false);
        _crashScreen.SetActive(true);
    }

    void DeactivateLevelEndedScreens() {
        _winScreen.color = new Color(255,255,255,0);
        _loseScreen.color = new Color(255,255,255,0);
        _crashScreen.SetActive(false);
    }
}
