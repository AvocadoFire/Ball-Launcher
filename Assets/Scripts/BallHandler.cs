using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

        if (!Touchscreen.current.primaryTouch.press.isPressed) { return; }
        //called every frame but if finger not on screen there is no value so have to do if

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        Debug.Log(worldPosition);
       
    }
}
