using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class TilemapCollider2DHandler : MonoBehaviour
{
    private TilemapCollider2D tilemapCollider;

    void Start()
    {
        tilemapCollider = GetComponent<TilemapCollider2D>();
        tilemapCollider.isTrigger = false; // Set to true if you want to handle collisions via OnTriggerEnter2D
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision logic here
        Debug.Log("Collided with: " + collision.gameObject.name);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Handle logic when collision ends
        Debug.Log("Collision ended with: " + collision.gameObject.name);
    }
}