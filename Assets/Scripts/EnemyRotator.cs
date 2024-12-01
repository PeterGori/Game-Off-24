using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    bool FacingRight = true;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") != null)
        {
            if (transform.position.x > GameObject.Find("Player").transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                FacingRight = false;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                FacingRight = true;
            }
        }
    }
}
