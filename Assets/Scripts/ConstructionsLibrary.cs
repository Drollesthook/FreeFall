﻿using System.Collections.Generic;
using UnityEngine;

public class ConstructionsLibrary : MonoBehaviour {
    public static ConstructionsLibrary Instance => _instance;
    
    [SerializeField] List<Construction> _easyConstructions = new List<Construction>();
    [SerializeField] List<Construction> _hardConstructions = new List<Construction>();

    static ConstructionsLibrary _instance;

    void Awake() {
        _instance = this;
    }

    public Construction GetEasyConstruction() {
        int i = Random.Range(0, _easyConstructions.Count);
        return _easyConstructions[i];
    }
    
    public Construction GetHardConstruction() {
        int i = Random.Range(0, _hardConstructions.Count);
        return _hardConstructions[i];
    }

    /*public Construction GetSomeConstruction() {
        int i = Random.Range(0, 1);
        Construction construction;
        switch (i) {
            case 0:
                construction = GetEasyConstruction();
                break;
            case 1:
                construction = GetHardConstruction();
                break;
        }

        return construction;
    }*/
}
