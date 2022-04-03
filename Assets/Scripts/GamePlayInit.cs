using UnityEngine;

public class GamePlayInit : MonoBehaviour
{
    private void Awake()
    {
        GamePlayManager.ChangeGameState(GameState.Init);
        Debug.Log("Init GamePlay!");
        Destroy(gameObject);
    }
}
