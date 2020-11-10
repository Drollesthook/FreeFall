using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditorInternal;

using UnityEngine;

public class InputManager : MonoBehaviour {
    [SerializeField] float _yInputThreshold = default;

    public event Action<float> ScreenIsTouched;
    public event Action ScreenIsReleased;
    public static InputManager Instance => _instance;

    Vector2 _firstTouchPosition;
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

    public Vector2 GetFirstTouchPosition() {
        return _firstTouchPosition;
    }

    void CheckForInput() {
        if (Input.GetMouseButtonDown(0)) {
            _firstTouchPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            if (_firstTouchPosition.y > _yInputThreshold) return;
            ScreenIsTouched?.Invoke(_firstTouchPosition.x);
        }

        if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(0)) {
            ScreenIsReleased?.Invoke();
        }
    }
    
    
}
