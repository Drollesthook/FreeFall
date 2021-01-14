using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] Image _winScreen;
    [SerializeField] GameObject _gameplayScreen, _mainMenuScreen, _crashScreen;
    [SerializeField] TMP_Text _currentLevelText, _currentGoldText;
    [SerializeField] Image _currentLevelBG;
    [SerializeField] Sprite _currentLevelBGAmeture, _currentLevelBGPro;
    [SerializeField] int _proLevel;
    [SerializeField] Material _usualSkyboxMaterial, _proSkyboxMaterial;
    
    Color _endColor = new Color(255, 255, 255, 255);
    int _currentLevel;
    void Start() {
        _currentLevel = LevelManager.Instance.CurrentLevelNumber;
        GameManager.Instance.NewLevelStarted += OnNewLevelStarted;
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        GameManager.Instance.GameplayStarted += OnGameplayStarted;
        GameManager.Instance.PlayerCrashed += OnPlayerCrashed;
        LevelManager.Instance.LevelCompleted += OnLevelCompleted;
        UpdateText();
    }

    void OnDestroy() {
        GameManager.Instance.NewLevelStarted -= OnNewLevelStarted;
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
        GameManager.Instance.GameplayStarted -= OnGameplayStarted;
        GameManager.Instance.PlayerCrashed -= OnPlayerCrashed;
        LevelManager.Instance.LevelCompleted -= OnLevelCompleted;
    }

    void OnNewLevelStarted() {
        DeactivateLevelEndedScreens();
        _gameplayScreen.SetActive(false);
        _mainMenuScreen.SetActive(true);
        if (_currentLevel >= _proLevel) ChangeSkybox(_proSkyboxMaterial);
    }

    void OnGameplayStarted() {
        _mainMenuScreen.SetActive(false);
        _gameplayScreen.SetActive(true);
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
        _crashScreen.SetActive(false);
    }

    void UpdateText() {
        _currentLevelText.text = _currentLevel + " ";
        UpdateCurrentLevelBGSprite();
    }
    
    void UpdateCurrentLevelBGSprite() {
        if (_currentLevel < _proLevel)
            _currentLevelBG.sprite = _currentLevelBGAmeture;
        else _currentLevelBG.sprite = _currentLevelBGPro;
    }

    void ChangeSkybox(Material newMaterial) {
        RenderSettings.skybox = newMaterial;
    }
}
