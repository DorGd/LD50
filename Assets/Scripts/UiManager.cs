using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI blackHoleDescription;

    public GameObject humansContainer;
    
    public Texture2D tex;

    private Camera _camera;

    private int _currentDescriptionIndex = -1;

    private List<GameObject> _humansCreated;
    
    private readonly string[] _blackHoleDescriptions = 
    {
        "50 Cent",
        "Lemon Tree",
        "Canada",
        "Earth",
        "Sun",
        "Will Smith Slap",
        "Oblivion * Doom * You're screwed"
    };

    protected void Awake()
    {
        _humansCreated = new List<GameObject>();
        _camera = Camera.main;
        GamePlayManager.OnGameStateChange += OnGameStateChange;
        GamePlayManager.OnBlackHoleEats += OnBlackHoleEats;
        GamePlayManager.OnHumanFallToDeepSpace += OnHumanFallToDeepSpace;
    }

    private void OnBlackHoleEats(Collider food, GamePlayData data)
    {
        if (food.gameObject.layer == LayerMask.NameToLayer("Human"))
        {
            AbortHuman();
        }
    }
    
    private void OnHumanFallToDeepSpace()
    {
        AbortHuman();
    }

    private void OnGameStateChange(GamePlayData data)
    {
        switch (data.State)
        {
            case GameState.Init:
                Init(data.LiveHumens);
                break;
            case GameState.LevelTransition:
                ChangeDescription();
                ChangePlanetImage();
                break;
        }
    }

    private void Init(int humansCount)
    {
        DOTween.To(() => _camera.fieldOfView, x => _camera.fieldOfView = x, 60, 3).SetEase(Ease.OutBack);

        ChangeDescription();
        
        CreateHumans(humansCount);
    }

    private void CreateHumans(int count)
    {
        for (var i = 0; i < count; i++)
        {
            GameObject human = new GameObject("Human");

            human.transform.SetParent(humansContainer.transform);
        
            Image image = human.AddComponent<Image>();
        
            image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        
            human.transform.localScale = Vector3.one;

            RectTransform myRectTransform = human.GetComponent<RectTransform>();

            var position = myRectTransform.localPosition;

            position = new Vector3(position.x, position.y, 0);
            
            myRectTransform.localPosition = position;
            
            _humansCreated.Add(human);
        }
    }

    private void AbortHuman()
    {
        if (_humansCreated.Count > 0)
        {
            var human = _humansCreated[^1];

            _humansCreated.Remove(human);
            
            human.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InBack).onComplete += () => Destroy(human);
        }
    }
    
    private void ChangeDescription()
    {
        if (_currentDescriptionIndex + 1 <= _blackHoleDescriptions.Length)
        {
            _currentDescriptionIndex++;

            blackHoleDescription.text = _blackHoleDescriptions[_currentDescriptionIndex];
        }
    }

    private void ChangePlanetImage()
    {
        return;
    }
}
