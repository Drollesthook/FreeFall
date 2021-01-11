using Lean.Pool;

using UnityEngine;

public class ConstructionSpawnPoint : MonoBehaviour {
    [SerializeField] int __lastEasyLevel = default;

    GameObject _constructionPrefab, _currentConstruction;

    public void SpawnConstruction(int currentLevelNumber) {
        GetConstruction(currentLevelNumber);
        //_currentConstruction = LeanPool.Spawn(_constructionPrefab, transform.position, Quaternion.identity);
        _currentConstruction = Instantiate(_constructionPrefab, transform.position, Quaternion.identity);
    }
    
    public void Deactivate() {
        if (_currentConstruction == null) return;
        Destroy(_currentConstruction);
        print("should be destroyed");
        //LeanPool.Despawn(_currentConstruction);
    }
    
    void GetConstruction(int currentLevelNumber) {
        if (currentLevelNumber <= __lastEasyLevel)
            _constructionPrefab = ConstructionsLibrary.Instance.GetEasyConstruction();
        else 
            _constructionPrefab = ConstructionsLibrary.Instance.GetHardConstruction();
    }
}
