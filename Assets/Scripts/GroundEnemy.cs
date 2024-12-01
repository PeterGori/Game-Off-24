using System;
using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GroundEnemy : MonoBehaviour
{
    public float moveSpeed;
    public int maxHealth;
    public float currentHealth;
    public GameObject leftPoint;
    public GameObject rightPoint;
    Vector3 velocity;
    private bool isMovingRight = true;

    public EnemyHealthBar healthBar;
    System.Random rnd = new System.Random();

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (isMovingRight)
        {
            if (Mathf.Abs(rightPoint.transform.position.x - gameObject.transform.position.x) <= 0.5f)
            {
                isMovingRight = false;
                SpriteFlip("left");
            }
            else
            {
                Move("right", moveSpeed);
            }
        }
        else
        {
            if (Mathf.Abs(leftPoint.transform.position.x - gameObject.transform.position.x) <= 0.5f)
            {
                isMovingRight = true;
                SpriteFlip("right");
            }
            else
            {
                Move("left", moveSpeed);
            }
        }
    }

    void Move(string direction, float moveSpeed)
    {
        if (direction == "left")
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else if (direction == "right")
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    private void SpriteFlip(string direction)
    {
        if (direction == "left")
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction == "right")
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(leftPoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(rightPoint.transform.position, 0.5f);
    }

    public void Death()
    {
        var position = gameObject.transform;
        var name = gameObject.name;
        Destroy(gameObject);
        PowerupSpawning.SpawnPowerup(rnd.Next(1,4), position, name);
        Debug.Log("step1");
    }
}