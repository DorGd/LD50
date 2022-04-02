using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BlackHole : MonoBehaviour
{
    private Action<Collider> BlackHoldEat;
    
    [SerializeField]
    private Rigidbody rb;

    private void Awake()
    {
        BlackHoldEat += OnBlackHoleEats;
    }

    private void OnTriggerEnter(Collider other)
    {
        BlackHoldEat.Invoke(other);
    }

    void OnBlackHoleEats(Collider food)
    {
        AbsorbFood(food);
    }

    private void AbsorbFood(Collider food)
    {
        float foodMass = food.attachedRigidbody.mass;
        Destroy(food.gameObject);
        rb.mass += foodMass * 10;
        transform.localScale += Vector3.one * foodMass;
    }
}
