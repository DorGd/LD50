using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameState : MonoBehaviour
{
    [SerializeField]
    private GameState State;

    private GameState _currentState;

    private void Awake()
    {
        _currentState = State;
    }

    private void Update()
    {
        if (_currentState != State)
        {
            GamePlayManager.ChangeGameState(State);
            _currentState = State;
        }
    }
}
