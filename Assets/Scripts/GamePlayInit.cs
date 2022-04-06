using UnityEngine;

public class GamePlayInit : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Init GamePlay!");
        GamePlayManager.ChangeGameState(GameState.Init, () => GamePlayManager.ChangeGameState(GameState.Play));
        SoundManager.Instance.PlayMusic(SoundManager.Instance.BgMusic);
        Destroy(gameObject);
    }
}
