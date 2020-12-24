using Lean.Pool;

using UnityEngine;

public class ConstructionSpawnPoint : MonoBehaviour {
    [SerializeField] int __lastEasyLevel = default;

    Construction _construction;

    public void SpawnConstruction(int currentLevelNumber) {
        GetConstruction(currentLevelNumber);
        LeanPool.Spawn(_construction, transform.position, Quaternion.identity);
    }
    
    public void Deactivate() {
        LeanPool.Despawn(_construction);
    }
    
    void GetConstruction(int currentLevelNumber) {
        if (currentLevelNumber <= __lastEasyLevel)
            _construction = ConstructionsLibrary.Instance.GetEasyConstruction();
        else 
            _construction = ConstructionsLibrary.Instance.GetHardConstruction();
    }
}
