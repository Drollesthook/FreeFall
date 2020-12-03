using System;
using System.Collections;
using System.Collections.Generic;

//using UnityEditorInternal;

using UnityEngine;

public class InputManager : MonoBehaviour {
    [SerializeField] float _yInputThreshold = default;

    public event Action<float> ScreenIsTouched;
    public event Action ScreenIsReleased;
    public static InputManager Instance => _instance;

    Vector2 _firstTouchPosition, _currentTouchPosition;
    bool _isScreenTouched;
    static InputManager _instance;
    Camera _mainCamera;

    void Awake() {
        _instance = this;
        _mainCamera = Camera.main;
    }

    void Update(){
        CheckForInput();
    }

    void CheckForInput() {
        if (Input.GetMouseButton(0)) {
            _currentTouchPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            if (_currentTouchPosition.y > _yInputThreshold) return;
            ScreenIsTouched?.Invoke(_currentTouchPosition.x);
        }

        if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(0)) {
            ScreenIsReleased?.Invoke();
        }
    }
    
    
}
