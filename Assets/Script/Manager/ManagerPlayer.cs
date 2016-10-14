using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerPlayer : MonoBehaviour
{
    public Image[] select;

    public ManagerPlayer man;

    public GameObject obj;

    public GameObject obj2;

    int x;

    bool podeDpad = false;

    void Start()
    {
        PlayerPrefs.SetInt("Players", 0);
        for (int i = 0; i < select.Length; i++)
        {
            if (i == x)
            {
                select[i].color = new Color(1f, 0.68f, 0.41f, 1);
            }
            else
            {
                select[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    void Update()
    {
        if (podeDpad)
        {
            if (Input.GetAxisRaw("DpadYP1") < 0 || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Proximo();
                podeDpad = false;
            }
            else if (Input.GetAxisRaw("DpadYP1") > 0 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Anterior();
                podeDpad = false;
            }
        }

        if (Input.GetAxisRaw("DpadYP1") == 0)
        {
            podeDpad = true;
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Return))
        {
            switch (x)
            {
                case 0:
                    Um();
                    break;

                case 1:
                    Dois();
                    break;

                case 2:
                    Tres();
                    break;

                case 3:
                    Quatro();
                    break;
            }
        }
    }

    void Anterior()
    {
        if (x > 0)
        {
            x--;
        }
        for (int i = 0; i < select.Length; i++)
        {
            if (i == x)
            {
                select[i].color = new Color(1f, 0.68f, 0.41f, 1);
            }
            else
            {
                select[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    void Proximo()
    {
        if (x < (select.Length - 1))
        {
            x++;
        }
        for (int i = 0; i < select.Length; i++)
        {
            if (i == x)
            {
                select[i].color = new Color(1f, 0.68f, 0.41f, 1);
            }
            else
            {
                select[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void Um()
    {
        PlayerPrefs.SetInt("Players", 1);
        SceneManager.LoadScene("SelecaoPersonagem");
    }

    public void Dois()
    {
        if (Input.GetJoystickNames().Length > 1)
        {
            PlayerPrefs.SetInt("Players", 2);
            SceneManager.LoadScene("SelecaoPersonagem");
        }
    }

    public void Tres()
    {
        obj.SetActive(true);
        man.enabled = false;
        obj2.SetActive(false);
    }

    public void Quatro()
    {
        Application.Quit();
    }
}
