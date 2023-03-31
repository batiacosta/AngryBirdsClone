using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Serialization;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Pivot : MonoBehaviour
{
    [SerializeField] private Bird currentBird;
    [SerializeField] private float factor;
    
    private Rigidbody2D _birdRigidBody2D;
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector3 _initial;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        SetNewBird();
    }


    private void Update() 
    {
        if(currentBird == null) return;
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (_isDragging)
            {
                ShootBird();
            }

            _isDragging = false;
            return;
        }
        _isDragging = true;
        if (currentBird.State != Bird.BirdState.Released)
        {
            SetBirdPositionWhilePressed();
        }
    }

    private void SetBirdPositionWhilePressed()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldFingerPosition = _mainCamera.ScreenToWorldPoint(touchPosition);
        currentBird.State = Bird.BirdState.Pressed;
        _birdRigidBody2D.position = worldFingerPosition;
    }

    private void SetNewBird()
    {
        _isDragging = false;
        _birdRigidBody2D = currentBird.GetComponent<Rigidbody2D>();
    }

    private void ShootBird()
    {
        currentBird.State = Bird.BirdState.Shooting;
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition = _birdRigidBody2D.position;
        Vector3 direction = initialPosition - finalPosition;
        _birdRigidBody2D.velocity = new Vector2(direction.x, direction.y) * factor;
        ReleaseBird();
    }
    private void ReleaseBird()
    {
        currentBird.State = Bird.BirdState.Released;
        currentBird = null;
    }
    
}
