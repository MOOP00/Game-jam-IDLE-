using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bulllet : MonoBehaviour
{
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter2D(Collision2D Collision)
    {
        Destroy(Collision.gameObject);
        Destroy(gameObject);

    }
}