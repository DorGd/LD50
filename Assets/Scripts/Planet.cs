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
    [SerializeField] private Collider _collider;
    
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

    private void OnDisable()
    {
        GamePlayManager.OnGameStateChange -= OnGameStateChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<AttractorTarget>() || other.isTrigger) return;
        _safeHumans.Add(other.attachedRigidbody);
        GamePlayManager.HumanEnteredSafePlanet();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<AttractorTarget>() || other.isTrigger) return;
        _safeHumans.Remove(other.attachedRigidbody);
        GamePlayManager.HumanEnteredSafePlanet(true);
    }

    private void OnGameStateChange(GamePlayData data)
    {
        switch (data.State)
        {
            case GameState.Init:
                OnInit(data);
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                break;
            case GameState.LevelTransition:
                OnPlanetTransition(data);
                break;
            case GameState.Lose:
                OnLose();
                break;
        }
    }

    private void OnInit(GamePlayData data)
    {
        if (data == null)
        {
            return;
        }
        if (_planetIndex == data.PlanetIndex)
        {
            _collider.enabled = false;
            StartCoroutine(nameof(PlanetTick));
        }
        else if (_planetIndex - data.PlanetIndex == 1)
        {
            _collider.enabled = true;
        }
    }

    private IEnumerator PlanetTick()
    {
        while (_rb.mass > 1)
        {
            _rb.mass -= 0.25f;
            yield return new WaitForSeconds(5f);
        }
        KillPlanet();
    }
    

    private void OnLose()
    {
        KillPlanet();
    }

    private void KillPlanet()
    {
        if (_rb != null)
        {
            _rb.mass = 5;
            _rb.isKinematic = false;
            _rb.AddForce((_BlackHoleAnchore.position - transform.position) * 200);
        }
    }

    private void OnPlanetTransition(GamePlayData data)
    {
        if (_planetIndex == data.PlanetIndex)
        {
            _rb.mass = 5;
            foreach (var human in _safeHumans)
            {
                if (human != null)
                {
                    human.transform.parent.parent = transform;
                }
            }

            gameObject.transform.DOMove(_currentPlanetAnchore.position, 5f).onComplete += (() =>
            {
                foreach (var human in _safeHumans)
                {
                    human.transform.parent.parent = null;
                }
                
                StartCoroutine(nameof(PlanetTick));
                GamePlayManager.ChangeGameState(GameState.Play);
            });
            _collider.enabled = false;
        }
        else if (_planetIndex - data.PlanetIndex == 1)
        {
            gameObject.transform.DOMove(_NextPlanetAnchore.position, 5f);
            _collider.enabled = true;
        }
        else if (_planetIndex - data.PlanetIndex == -1)
        {
            KillPlanet();
        }
    }
}
