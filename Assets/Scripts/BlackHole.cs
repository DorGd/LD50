using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BlackHole : MonoBehaviour
{
    
    [SerializeField]
    private Rigidbody rb;

    private void Awake()
    {
        GamePlayManager.OnBlackHoleEats += OnBlackHoleEats;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            GamePlayManager.BlackHoleEats(other);
        }
    }

    void OnBlackHoleEats(Collider food, GamePlayData data)
    {
        Debug.Log($"Black Hole eats {food.transform.parent.name}");
        AbsorbFood(food, data);
    }

    private void AbsorbFood(Collider food, GamePlayData data)
    {
        float foodMass = food.attachedRigidbody.mass;
        rb.mass += foodMass * 10;
        transform.localScale += Vector3.one * foodMass;
        data.BlackHoleMass = rb.mass;
        Destroy(food.gameObject);
    }
}
