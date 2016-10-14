using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) || Input.GetKeyDown(KeyCode.Escape))
        {
            Para();
        }
    }

    public void Para()
    {
        obj.SetActive(true);
        Time.timeScale = 0;
    }    
}
