using UnityEngine;

public class GamePlayInit : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Init GamePlay!");
        GamePlayManager.ChangeGameState(GameState.Init, () => GamePlayManager.ChangeGameState(GameState.Play));
        Destroy(gameObject);
    }
}
