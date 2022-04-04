using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] private Transform _currentPlanetAnchore;
    [SerializeField] private Transform _NextPlanetAnchore;
    [SerializeField] private Transform _BlackHoleAnchore;
    [SerializeField] private int _planetIndex;
    
    private Rigidbody _rb;
    private List<Rigidbody> _safeHumans;
    private void Awake()
    {
        GamePlayManager.OnGameStateChange += OnGameStateChange;
        _rb = GetComponentInChildren<Rigidbody>();
        _safeHumans = new List<Rigidbody>();

        if (_rb == null)
        {
            Debug.LogError($"{gameObject.name} can't find rigid body!");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<AttractorTarget>() || other.isTrigger) return;
        Debug.Log("HumanEnteredSafePlanet -> " + gameObject.name);
        _safeHumans.Add(other.attachedRigidbody);
        GamePlayManager.HumanEnteredSafePlanet();
    }

    private void OnGameStateChange(GamePlayData data)
    {
        switch (data.State)
        {
            case GameState.Init:
                if (_planetIndex == data.PlanetIndex)
                {
                    gameObject.transform.DOMove(_currentPlanetAnchore.position, 5f);
                    GetComponent<Collider>().enabled = false;
                }
                else if (_planetIndex - data.PlanetIndex == 1)
                {
                    gameObject.transform.DOMove(_NextPlanetAnchore.position, 5f);
                    GetComponent<Collider>().enabled = true;
                }
                else
                {
                    // Prevents Attraction.
                }
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                break;
            case GameState.LevelTransition:
                if (_planetIndex == data.PlanetIndex)
                {
                    foreach (var human in _safeHumans)
                    {
                        human.transform.parent.parent = transform;
                        // human.isKinematic = false;
                    }
                    gameObject.transform.DOMove(_currentPlanetAnchore.position, 5f).onComplete += (() =>
                    {
                        foreach (var human in _safeHumans)
                        {
                            human.transform.parent.parent = null;
                            // human.isKinematic = false;
                        }
                        GamePlayManager.ChangeGameState(GameState.Play);
                    });
                    GetComponent<Collider>().enabled = false;
                }
                else if (_planetIndex - data.PlanetIndex == 1)
                {
                    gameObject.transform.DOMove(_NextPlanetAnchore.position, 5f);
                    GetComponent<Collider>().enabled = true;
                }
                else if (_planetIndex - data.PlanetIndex == -1)
                {
                    // For debug
                    gameObject.transform.DOMove(_BlackHoleAnchore.position, 3f).SetEase(Ease.OutBounce).onComplete += () => Destroy(gameObject);
                }
                break;
        }
    }

    private IEnumerator MagnetHumanity(bool magnetState, float duration = -1)
    {
        int factor = magnetState ? 1 : -1;
        _rb.mass += factor * 200;

        if (duration > 0)
        {
            yield return new WaitForSeconds(duration);
            _rb.mass -= factor * 100;
        }
    }
}
