

using System;
using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public int maxHealth;
    public float currentHealth;
    Vector3 velocity;
    public EnemyHealthBar healthBar;
    static System.Random rnd = new System.Random();
    private bool playerInSight = false;
    private int num = rnd.Next(1, 4);

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Vector2.Distance(GameObject.Find("Player").transform.position, gameObject.transform.position) < 80)
        {
            playerInSight = true;
        }
        else playerInSight = false;

        if (playerInSight) Attack();
    }

    private void Attack()
    {
        Debug.Log("Attacking Player");
        var choice = num switch
        {
            1 => Fireball(),
            2 => FireballSpread(),
            3 => Nothing(),
        };
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

    int Fireball()
    {
        Instantiate(GameObject.Find("Boss").GetComponent<Fireball>().FireballPrefab, GameObject.Find("Boss").transform.position, GameObject.Find("Boss").transform.rotation);
        return 0;
    }
    
    int FireballSpread()
    {
        Instantiate(GameObject.Find("Boss").GetComponent<Fireball>().FireballPrefab, GameObject.Find("Boss").transform.position, Quaternion.Euler(0, 0, 10));
        Instantiate(GameObject.Find("Boss").GetComponent<Fireball>().FireballPrefab, GameObject.Find("Boss").transform.position, GameObject.Find("Boss").transform.rotation);
        Instantiate(GameObject.Find("Boss").GetComponent<Fireball>().FireballPrefab, GameObject.Find("Boss").transform.position, Quaternion.Euler(0, 0, -10));

        return 0;
    }
    
    int Nothing()
    {
        return 0;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
