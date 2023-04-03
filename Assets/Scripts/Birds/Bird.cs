using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Bird : MonoBehaviour
{
    public enum BirdState
    {
        Idle,
        Pressed,
        Shooting,
        Released,
        Hit,
        Activated
    }

    public BirdState State
    {
        get => _state;
        set
        {
            _state = value;
            SetBirdState();
        }
    }

    [SerializeField] private SpriteRenderer birdSprite;
    [SerializeField] private BirdSO birdSO;

    private BirdState _state = BirdState.Idle;
    public Rigidbody2D BirdRigidbody2D { get; private set; } 
    public bool IsAbilityActivated { get; private set; } 
    private void OnEnable()
    {
        birdSprite.sprite = birdSO.birdImage;
        BirdRigidbody2D = GetComponent<Rigidbody2D>();
        State = BirdState.Idle;
        IsAbilityActivated = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        State = BirdState.Hit;
        IsAbilityActivated = true;
    }

    private void Update()
    {
        if (_state == BirdState.Released)
        {
            CheckForPowerUp();
        }
    }

    private void CheckForPowerUp()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed && !IsAbilityActivated)
        {
            State = BirdState.Activated;
            IsAbilityActivated = true;
        }
    }

    private void SetBirdState()
    {
        switch (State)
        {
            case BirdState.Idle:
                BirdRigidbody2D.isKinematic = true;
                SetIdle();
                break;
            case BirdState.Pressed:
                BirdRigidbody2D.isKinematic = true;
                SetPressed();
                    break;
            case BirdState.Shooting:
                BirdRigidbody2D.isKinematic = false;
                SetShooting();
                break;
            case BirdState.Released:
                SetReleased();
                break;
            case BirdState.Hit:
                SetHit();
                break;
            case BirdState.Activated:
                SetActivated();
                break;
            default: break;
        }
    }

    public virtual void SetHit()
    {
        //  Play animations when bird hits something
        //  Play animations when bird hits something
    }

    public virtual void SetActivated()
    {
        //  Play animations when special ability is activated
        //  Play animations when special ability is activated
    }

    public virtual void SetReleased()
    {
        //  Play animations when released
        //  Play animations when released
    }

    public virtual void SetShooting()
    {
        //  Play animations when bird is shot
        //  Play animations when bird is shot
    }

    public virtual void SetPressed()
    {
        //  Play animations when pressed
        //  Play animations when pressed
    }

    public virtual void SetIdle()
    {
        //  Play animations for idle
        //  Play animations for idle
    }
    


}
