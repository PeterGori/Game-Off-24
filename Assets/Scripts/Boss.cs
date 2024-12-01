

using System;
using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public int maxHealth;
    public float currentHealth;
    Vector3 velocity;
    public EnemyHealthBar healthBar;
    System.Random rnd = new System.Random();

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
        healthBar.SetHealth(currentHealth);
    }

    public void Death()
    {
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
    }
}
