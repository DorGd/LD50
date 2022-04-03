using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;

public enum GameState
{
    Init,
    Play,
    Pause,
    LevelTransition,
    Lose,
    Win,
    Exit
}
public static class GamePlayManager
{
    public static event Action<GameState> ChangeGameState;
    
    private static GameState CurrentState;

    private static GamePlayData GameData;
    
    public static void OnGameStateChange(GameState state)
    {
        if (CurrentState == state)
        {
            return;
        }

        CurrentState = state;

        switch (state)
        {
            case GameState.Init:
                OnInit();
                break;
            case GameState.Play:
                OnPlay();
                break;
            case GameState.Pause:
                OnPause();
                break;
            case GameState.LevelTransition:
                OnLevelTransition();
                break;
            case GameState.Lose:
                OnLose();
                break;
            case GameState.Win:
                OnWin();
                break;
            case GameState.Exit:
                OnExit();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    static void OnInit()
    {
        GameData = AssetDatabase.LoadAssetAtPath<GamePlayData>("Assets/Scripts");
        if (GameData == null)
        {
            Debug.LogError("Can't find GameData, please check path is correct.");
        }
    }
    
    static void OnPlay()
    {
        throw new NotImplementedException();
    }
    
    static void OnPause()
    {
        throw new NotImplementedException();
    }
    
    static void OnLevelTransition()
    {
        throw new NotImplementedException();
    }
    
    static void OnLose()
    {
        throw new NotImplementedException();
    }
    
    static void OnWin()
    {
        throw new NotImplementedException();
    }
    
    static void OnExit()
    {
        throw new NotImplementedException();
    }
}
