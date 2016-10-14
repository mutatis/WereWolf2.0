using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpcoesNavigat : MonoBehaviour
{

    public Controllopcao[] efeito;

    public GameObject obj;
    public GameObject obj2;

    public ManagerPlayer man;

    int x;

    bool podeDpad;
    bool editando = false;
    
    void Start()
    {
        efeito[x].Select();
    }

	void Update ()
    {
        if (podeDpad && !editando)
        {
            if (Input.GetAxisRaw("DpadYP1") < 0 || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Proximo();
            }
            else if (Input.GetAxisRaw("DpadYP1") > 0 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Anterior();
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Escape))
            {
                obj.SetActive(true);
                man.enabled = true;
                obj2.SetActive(false);
            }
        }

        if(!editando && (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return)))
        {
            editando = true;
        }

        if(editando && podeDpad)
        {
            if(Input.GetAxisRaw("DpadXP1") > 0 || Input.GetKeyDown(KeyCode.RightArrow))
            {
                efeito[x].Aumenta();
                podeDpad = false;
            }
            else if (Input.GetAxisRaw("DpadXP1") < 0 || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                efeito[x].Diminui();
                podeDpad = false;
            }
            else if(Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Escape))
            {
                editando = false;
            }
        }

        if (Input.GetAxisRaw("DpadYP1") == 0 && Input.GetAxisRaw("DpadXP1") == 0 || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow)
             || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            podeDpad = true;
        }
    }

    void Anterior()
    {
        podeDpad = false;
        if (x > 0)
        {
            x--;
        }
        for (int i = 0; i < efeito.Length; i++)
        {
            if (i == x)
            {
                efeito[i].Select();
            }
            else
            {
                efeito[i].Deselect();
            }
        }
    }

    void Proximo()
    {
        podeDpad = false;
        if (x < (efeito.Length - 1))
        {
            x++;
        }
        for (int i = 0; i < efeito.Length; i++)
        {
            if (i == x)
            {
                efeito[i].Select();
            }
            else
            {
                efeito[i].Deselect();
            }
        }
    }
}
