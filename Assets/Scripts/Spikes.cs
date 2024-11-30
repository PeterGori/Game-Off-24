using System;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject player;
    private Mesh mesh;
    public int Damage;
    private float time;
    readonly float interpolationPeriod = 1f;


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player hit spikes");
            GameObject.Find("Player").GetComponent<Player>().TakeDamage(Damage);
        }
    }
}
