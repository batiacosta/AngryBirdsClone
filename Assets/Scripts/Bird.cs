using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private BirdState _state;
    private Rigidbody2D _rigidbody2D;
    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        State = BirdState.Idle;
    }
    
    private void SetBirdState()
    {
        switch (State)
        {
            case BirdState.Idle: 
                _rigidbody2D.isKinematic = true;
                SetIdle();
                break;
            case BirdState.Pressed:
                _rigidbody2D.isKinematic = true;
                SetPressed();
                    break;
            case BirdState.Shooting:
                _rigidbody2D.isKinematic = false;
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

    private void SetHit()
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
