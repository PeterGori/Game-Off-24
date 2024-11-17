using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;

public class CollisionController : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    
    public float interpolationPeriod = 1f;
    private float time = 0f;
    public int Damage;
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= interpolationPeriod)
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
        Debug.Log("Enemy attacked");
    }
}
