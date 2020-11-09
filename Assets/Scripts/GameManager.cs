using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance => _instance;
    public int PlayerSpeed => _playerSpeed;
    
    [SerializeField] int _playerSpeed;

    static GameManager _instance;
    void Start() {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
