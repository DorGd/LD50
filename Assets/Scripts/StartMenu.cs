using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI headLine;
    [SerializeField] private Image buttonImg;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        SoundManager.Instance.PlayMusic(SoundManager.Instance.OpeningMusic);
    }

    public void StartGame()
    {
        DOTween.KillAll();
        headLine.DOFade(0, 1.5f).SetEase(Ease.OutSine);
        buttonImg.DOFade(0, 1.5f).SetEase(Ease.OutSine);
        buttonText.DOFade(0, 1.5f).SetEase(Ease.OutSine).onComplete
            +=() => bg.DOFade(0, 1.5f).SetEase(Ease.OutSine).onComplete
                += () => DOTween.To(() => _camera.fieldOfView, x => _camera.fieldOfView = x, 179,4).SetEase(Ease.InCubic).onComplete
                    += () => 
                    {
                        SceneManager.LoadScene(1);
                    };
    }
}
