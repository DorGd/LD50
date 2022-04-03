using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(GamePlayData), fileName = "Create GamePlay Data")]
public class GamePlayData : ScriptableObject
{
    public GameState State;
    public int LiveHumens;
    public float BlackHoleMass;
    public int PlanetIndex = 0;
    public GameObject BlackHole;
    public int SafeHumans = 0;

    public void Print()
    {
        Debug.Log($"State: {State}, Lives: {LiveHumens}, Safe: {SafeHumans}, BH mass: {BlackHoleMass}, Planet Idx: {PlanetIndex}");
    }
}
