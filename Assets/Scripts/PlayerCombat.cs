using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerAttack();
        }
    }

    void PlayerAttack()
    {
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Apply damage to enemies
        foreach (Collider2D enemy in HitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<GroundEnemy>().TakeDamage(Player.damage * Player.damageModifier);
        }
        Debug.Log("Player attacked");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

