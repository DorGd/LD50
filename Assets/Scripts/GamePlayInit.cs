using UnityEngine;

public class GamePlayInit : MonoBehaviour
{
    private void Awake()
    {
        GamePlayManager.ChangeGameState(GameState.Init, () => GamePlayManager.ChangeGameState(GameState.Play));
        Debug.Log("Init GamePlay!");
        Destroy(gameObject);
    }
}
