using UnityEngine;
using System.Collections;

public class WorldSwitch : MonoBehaviour
{
    public bool worldSwitch_active = false;
    public Animator transition;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt) && worldSwitch_active == false)
        {
            StartCoroutine(WorldSwitchAnimation());
        }
    }

    public IEnumerator WorldSwitchAnimation()
    { 
        worldSwitch_active = true;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.9f);
        worldSwitch_active = false;
    }
}
