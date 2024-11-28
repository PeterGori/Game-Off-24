using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Timeline;

public class PowerUps : MonoBehaviour
{
    public Transform powerupPoint;
    public float pickupRange;
    public LayerMask enemyLayers;
    private GameObject player;
    
    public float interpolationPeriod = 0.1f;
    private float time = 0f;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null) Debug.Log("null player");
    }

    void Update()
    {
        if (player == null) return;
        time += Time.deltaTime;
        Debug.Log(player.transform.position);
        Debug.Log(powerupPoint.transform.position);
        if (time >= interpolationPeriod && Mathf.Abs(Vector2.Distance(powerupPoint.transform.position, player.transform.position)) < (pickupRange + 0.5))
        {
            Debug.Log("Should Pickup Powerup");
            PowerUp();
            time = 0f;
        }
    }
    
    void PowerUp()
    {
        if (gameObject.tag == "Strength") Player.damageModifier += 0.1f;
        else if (gameObject.tag == "Speed") Player.moveSpeed += 0.2f;
        else if (gameObject.tag == "Health") player.GetComponent<Player>().Heal(100);
        Destroy(gameObject);
    }
}
