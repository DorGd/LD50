using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] private Transform _currentPlanetAnchore;
    [SerializeField] private Transform _NextPlanetAnchore;
    [SerializeField] private Transform _BackStagePlanetAnchore;
    [SerializeField] private Transform _BlackHoleAnchore;
    [SerializeField] private int _planetIndex;
    [SerializeField] private Rigidbody rb;

    private GamePlayData _gameData;

    private void Awake()
    {
        GamePlayManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GamePlayData data)
    {
        switch (data.State)
        {
            case GameState.Init:
                if (_planetIndex == data.PlanetIndex)
                {
                    gameObject.transform.DOMove(_currentPlanetAnchore.position, 5f);
                }
                else if (_planetIndex - data.PlanetIndex == 1)
                {
                    gameObject.transform.DOMove(_NextPlanetAnchore.position, 5f);
                }
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                break;
            case GameState.LevelTransition:
                if (_planetIndex == data.PlanetIndex)
                {
                    gameObject.transform.DOMove(_currentPlanetAnchore.position, 5f);
                }
                else if (_planetIndex - data.PlanetIndex == 1)
                {
                    gameObject.transform.DOMove(_NextPlanetAnchore.position, 5f);
                }
                else
                {
                    // For debug
                    gameObject.transform.DOMove(_BlackHoleAnchore.position, 3f).SetEase(Ease.OutBounce).onComplete += () => Destroy(gameObject);
                }
                break;

        }
    }
}
