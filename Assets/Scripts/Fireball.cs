using UnityEngine;

public class Fireball : MonoBehaviour
{
    

    private float time = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 5)
        {
            Destroy(gameObject);
        }
        if (Vector2.Distance(gameObject.transform.position, GameObject.Find("Boss").transform.position) > 80)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("Player").transform.position, 0.1f);
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
}
