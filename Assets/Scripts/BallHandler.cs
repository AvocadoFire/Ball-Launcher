using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Rigidbody2D pivot;
    [SerializeField] float detachDelay = .1f;
    [SerializeField] float respawnDelay = 1f;

    Rigidbody2D currentBallRigidbody;
    SpringJoint2D currentBallSpringJoint;

    private Camera mainCamera;
    private bool isDragging;

    private void Start()
    {
       mainCamera = Camera.main;
       SpawnNewBall();
    }

    void Update()
    {
        if(currentBallRigidbody == null){ return; }
        if (!Touchscreen.current.primaryTouch.press.isPressed) 
            //called every frame but if finger not on screen there is no value so have to do if return
        { 
            if (isDragging)
            {
                LaunchBall();
            }
            isDragging = false;
            return;
        }
        isDragging = true;
        currentBallRigidbody.isKinematic = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
       currentBallRigidbody.position = worldPosition;
    }

    private void SpawnNewBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);
        currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();
        currentBallSpringJoint.connectedBody = pivot;
    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false; ///make body react to physics again
        currentBallRigidbody = null; //doesn't matter if touch screen

        Invoke(nameof(DetachBall), detachDelay);
        Invoke(nameof(SpawnNewBall), respawnDelay);
    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}
