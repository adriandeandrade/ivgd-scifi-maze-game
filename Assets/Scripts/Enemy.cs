﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;

    public void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            health -= damage;
        }
    }
}
