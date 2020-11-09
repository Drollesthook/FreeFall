using System.Collections;
using System.Collections.Generic;

using UnityEditorInternal;

using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager Instance => _instance;
    Vector2 _firstTouchPosition, _currentTouchPosition;

    static InputManager _instance;

    void Awake() {
        _instance = this;
    }

    void Update()
    {
        CheckForInput();
    }

    public Vector2 GetFirstTouchPosition() {
        return _firstTouchPosition;
    }

    public Vector2 GetCurrentTouchPosition() {
        return _currentTouchPosition;
    }

    void CheckForInput() {
        if (Input.GetMouseButtonDown(0)) {
            _firstTouchPosition = Input.mousePosition;
            print(_firstTouchPosition);
        }
    }
    
    
}
