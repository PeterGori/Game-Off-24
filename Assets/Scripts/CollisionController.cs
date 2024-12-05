using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;
// ReSharper disable All

public class CollisionController : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    
    public float interpolationPeriod = 1f;
    private float time = 0f;
    public int Damage;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        if (player == null) Debug.Log("null player");
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= interpolationPeriod && Vector2.Distance(attackPoint.position, player.transform.position) < (attackRange + 0.1))
        {
            Attack();
            time = 0f;
        }
    }
    
    
    private void Attack()
    {
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in HitEnemies)
        {
            Debug.Log("Enemy hit " + enemy.name);
            enemy.GetComponent<Player>().TakeDamage(Damage);
        }
    }
}
