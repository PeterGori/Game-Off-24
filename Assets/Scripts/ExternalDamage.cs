using UnityEngine;

public class ExternalDamage : MonoBehaviour
{
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DamagePlayer(int damage)
    {
        player.GetComponent<Player>().TakeDamage(damage);
    }
}
