using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Transform spikecenter;
    public float attackRange;
    public LayerMask enemyLayers;
    private GameObject player;
    private Mesh mesh;
    public int Damage;
    private float time;
    readonly float interpolationPeriod = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null) Debug.Log("null player");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interpolationPeriod && spikecenter.position.y == player.transform.position.y)
        {
            if (Vector2.Distance(player.transform.position, mesh.bounds.ClosestPoint(player.transform.position)) < (spikecenter.transform.localScale.magnitude) / 2)
            {
                if (!(player.transform - mesh.bounds.ClosestPoint(Vector3.left) < 0))
                {
                    
                }
            }
        }
    }

    private void Attack()
    {
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(spikecenter.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in HitEnemies)
        {
            Debug.Log("Spike hit " + enemy.name);
            enemy.GetComponent<Player>().TakeDamage(Damage);
        }
    }
}
