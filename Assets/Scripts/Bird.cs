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
                break;
            case BirdState.Pressed:
                _rigidbody2D.isKinematic = true;
                    break;
            case BirdState.Shooting:
                _rigidbody2D.isKinematic = false;
                break;
            case BirdState.Released:
                break;
            case BirdState.Activated:
                break;
            default: break;
        }
    }

    
}
