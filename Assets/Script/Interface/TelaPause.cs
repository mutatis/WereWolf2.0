using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TelaPause : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick2Button0) || 
			Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7)||
            Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            Resume();
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick2Button1) ||
            Input.GetKeyDown(KeyCode.C))
        {
            Menu();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        obj.SetActive(false);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}