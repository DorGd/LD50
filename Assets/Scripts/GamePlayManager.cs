using System;
using UnityEditor;
using UnityEngine;

public enum GameState
{
    Undefined,
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
    public static event Action<GamePlayData> OnGameStateChange;
    public static event Action<GamePlayData> OnHumanEnteredSafePlanet;
    public static event Action<Collider, GamePlayData> OnBlackHoleEats;
    public static event Action OnHumanFallToDeepSpace;
    
    private static GamePlayData _gameData;
    private static GameObject _human;
    private const int HumanityInitSize = 100;

    public static void ChangeGameState(GameState state, Action onComplete = null)
    {
        if (_gameData != null && _gameData.State == state)
        {
            return;
        }

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
        }
        _gameData.Print();
        OnGameStateChange?.Invoke(_gameData);
        Debug.Log($"Chane Game State to: {state}");
        onComplete?.Invoke();
    }

    static void OnInit()
    {
        _gameData = AssetDatabase.LoadAssetAtPath<GamePlayData>("Assets/ScriptableObjects/GameData.asset");
        if (_gameData == null)
        {
            Debug.LogError("Can't find GameData, please check path is correct.");
            return;
        }
        
        var planet = GameObject.Find("Planet_0");
        var human = Resources.Load<GameObject>("Prefabs/Human");
        for (int i = 0; i < HumanityInitSize; i++)
        {
            var position = planet.transform.position + Quaternion.AngleAxis( Mathf.Rad2Deg * 2 * Mathf.PI * ((float) (i % 25) / 25), Vector3.forward) *
                Vector3.up * (planet.transform.localScale.x + - (human.transform.localScale.x * Mathf.Floor( (float) i / 25)));
            UnityEngine.Object.Instantiate(human, position, Quaternion.identity);
        }
        
        _gameData.State = GameState.Init;
        _gameData.PlanetIndex = 0;
        _gameData.BlackHole = GameObject.Find("BlackHole");
        _gameData.BlackHoleMass = _gameData.BlackHole.GetComponentInChildren<Rigidbody>().mass;
        _gameData.LiveHumens = HumanityInitSize;
        _gameData.SafeHumans = 0;
    }
    
    static void OnPlay()
    {
        _gameData.State = GameState.Play;
    }
    
    static void OnPause()
    {
        _gameData.State = GameState.Pause;
    }
    
    static void OnLevelTransition()
    {
        _gameData.State = GameState.LevelTransition;
        _gameData.SafeHumans = 0;
        _gameData.PlanetIndex++;
    }
    
    static void OnLose()
    {
        _gameData.State = GameState.Lose;
    }
    
    static void OnWin()
    {
        _gameData.State = GameState.Win;
    }
    
    static void OnExit()
    {
        _gameData.State = GameState.Exit;
    }

    public static void HumanEnteredSafePlanet()
    {
        _gameData.SafeHumans++;
        OnHumanEnteredSafePlanet?.Invoke(_gameData);
        CheckHumanitySafe();
    }

    public static void HumanFallToDeepSpace()
    {
        _gameData.LiveHumens--;
        OnHumanFallToDeepSpace?.Invoke();
        var isDoomed = CheckHumanityDoomed();
        if (!isDoomed) CheckHumanitySafe();
    }
    
    public static void BlackHoleEats(Collider food)
    {
        if (food.gameObject.layer == LayerMask.NameToLayer("Human"))
        {
            _gameData.LiveHumens--;
        }
        OnBlackHoleEats?.Invoke(food, _gameData);
        var isDoomed = CheckHumanityDoomed();
        if (!isDoomed) CheckHumanitySafe();
    }

    private static bool CheckHumanityDoomed()
    {
        if (_gameData.LiveHumens == 0)
        {
            ChangeGameState(GameState.Lose);
            return true;
        }

        return false;
    }

    private static void CheckHumanitySafe()
    {
        if (_gameData.LiveHumens == _gameData.SafeHumans)
        {
            ChangeGameState(GameState.LevelTransition);
        }
    }
}
