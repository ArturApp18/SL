using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D _effector2D;
    
    private IInputService _input;
    private float _waitTime;

    public float WaitTimer = 0.5f;

    private void Awake()
    {
        _input = AllServices.Container.Single<IInputService>();
    }

    private void Update()
    {
        
        if (_input.Axis.y < -0.5f)
        {
            if (_waitTime <= 0)
            {
                _effector2D.rotationalOffset = 180f;
                _waitTime = WaitTimer;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }

        if (_input.IsJumpButton())
        {
            _effector2D.rotationalOffset = 0;
        }
    }
}
