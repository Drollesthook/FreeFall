using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance => _instance;
    public int WorldSpeed => _worldSpeed;
    
    [SerializeField] int _worldSpeed = default;

    static GameManager _instance;
    void Awake() {
        _instance = this;
    }

}
