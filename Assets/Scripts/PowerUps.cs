using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public Transform powerupPoint;
    public float pickupRange;
    public LayerMask enemyLayers;
    
    public float interpolationPeriod = 0.1f;
    private float time = 0f;
    void Update()
    {
        // if (gameObject.transform.position.x > GameObject.Find("Player").transform.position.x - 3 && gameObject.transform.position.x < GameObject.Find("Player").transform.position.x + 3)
        // {
        //     if (gameObject.transform.position.y > GameObject.Find("Player").transform.position.y - 3 && gameObject.transform.position.y < GameObject.Find("Player").transform.position.y + 3)
        //     {
        //         PowerUp();
        //     }
        // }
        time += Time.deltaTime;
        if (time >= interpolationPeriod && Vector2.Distance(powerupPoint.position, GameObject.Find("Player").transform.position) < (pickupRange + 0.1))
        {
            PowerUp();
            time = 0f;
        }
    }
    
    void PowerUp()
    {
        if (gameObject.name == "Strength") Player.damageModifier += 0.1f;
        else if (gameObject.name == "Speed") Player.moveSpeed += 0.2f;
        else if (gameObject.name == "Health")GameObject.Find("Player").GetComponent<Player>().Heal(100);
        Destroy(gameObject);
    }
}
