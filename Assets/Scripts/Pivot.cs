using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Pivot : MonoBehaviour
{
    [SerializeField] private Bird currentBird;
    [SerializeField] private float factor;
    [SerializeField] private InGameDataSO inGameDataSO;
    [SerializeField] private Transform placeToSpawn;
    
    private Rigidbody2D _birdRigidBody2D;
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector3 _initial;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        inGameDataSO.OnCurrentBirdSOChanged += SetNewBirdFromEvent;
        if (inGameDataSO.CurrentBirdSO != null)
        {
            SetNewCurrentBird(inGameDataSO.CurrentBirdSO);
        }
    }

    private void SetNewBirdFromEvent(object sender, BirdSO e)
    {
        Debug.Log("Se invocó desde el botón e inGameData");
        SetNewCurrentBird(e);
    }

    private void SetNewCurrentBird(BirdSO birdSO)
    {
        _isDragging = false;
        if (placeToSpawn.childCount != 0)
        {
            foreach (Transform child in placeToSpawn.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        var birdPrefab = Instantiate(birdSO.prefab, placeToSpawn, true);
        birdPrefab.SetPositionAndRotation(placeToSpawn.transform.position, Quaternion.identity );
        birdPrefab.transform.localScale *= 0.05f;
        currentBird = birdPrefab.GetComponent<Bird>();
        _birdRigidBody2D = currentBird.GetComponent<Rigidbody2D>();
        
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
        if (currentBird.State != Bird.BirdState.Released)
        {
            SetBirdPositionWhilePressed();
        }
    }

    private void SetBirdPositionWhilePressed()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _isDragging = true;
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldFingerPosition = _mainCamera.ScreenToWorldPoint(touchPosition);
            currentBird.State = Bird.BirdState.Pressed;
            _birdRigidBody2D.position = worldFingerPosition;
        }

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
