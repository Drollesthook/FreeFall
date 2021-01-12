using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] Image _winScreen, _loseScreen;
    [SerializeField] GameObject _gameplayScreen, _mainMenuScreen, _crashScreen;
    [SerializeField] TMP_Text _currentLevelText, _currentGoldText;
    
    Color _endColor = new Color(255, 255, 255, 255);
    int _currentLevel;
    void Start() {
        _currentLevel = LevelManager.Instance.CurrentLevelNumber;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        GameManager.Instance.LevelFailed += OnLevelFailed;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
        LevelManager.Instance.LevelCompleted += OnLevelCompleted;
        UpdateText();
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
        GameManager.Instance.LevelFailed -= OnLevelFailed;
        GameManager.Instance.GameplayStarted -= OnGameplayStarted;
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
        LevelManager.Instance.LevelCompleted -= OnLevelCompleted;
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

    void OnLevelCompleted(int currentLevel) {
        _currentLevel = currentLevel;
        UpdateText();
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

    void UpdateText() {
        _currentLevelText.text = _currentLevel + " ";
    }
}
