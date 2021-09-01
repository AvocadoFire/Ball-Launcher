using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] Rigidbody2D currentBallRigidbody;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

        if (!Touchscreen.current.primaryTouch.press.isPressed) 
            //called every frame but if finger not on screen there is no value so have to do if return
        { 
            currentBallRigidbody.isKinematic = false;
            return;
        }
        currentBallRigidbody.isKinematic = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        currentBallRigidbody.position = worldPosition;

    }
}
