using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class BlackHole : MonoBehaviour
{
    private Action<Collider> BlackHoldEat;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        BlackHoldEat += OnBlackHoleEats;
    }

    private void OnTriggerEnter(Collider other)
    {
        BlackHoldEat.Invoke(other);
    }

    void OnBlackHoleEats(Collider food)
    {
        float foodMass = food.attachedRigidbody.mass;
        Destroy(food.gameObject);
        rb.mass += foodMass;
    }

}
