using UnityEngine;

public class ConstructionSpawnPoint : MonoBehaviour {
    [SerializeField] int _lastEasyLevel = default;
    [SerializeField] bool _isAdditionalPoint = default;
    
    GameObject _constructionPrefab, _currentConstruction;

    public void SpawnConstruction(int currentLevelNumber) {
        if (_isAdditionalPoint) {
            if (currentLevelNumber < _lastEasyLevel)
                return;
            _constructionPrefab = ConstructionsLibrary.Instance.GetEasyConstruction();
            _currentConstruction = Instantiate(_constructionPrefab, transform.position, Quaternion.identity);
            return;
        }
        GetConstruction(currentLevelNumber);
        _currentConstruction = Instantiate(_constructionPrefab, transform.position, Quaternion.identity);
    }
    
    public void Deactivate() {
        if (_currentConstruction == null) return;
        Destroy(_currentConstruction);
    }
    
    void GetConstruction(int currentLevelNumber) {
        if (currentLevelNumber <= _lastEasyLevel)
            _constructionPrefab = ConstructionsLibrary.Instance.GetEasyConstruction();
        else 
            _constructionPrefab = ConstructionsLibrary.Instance.GetHardConstruction();
    }
}
