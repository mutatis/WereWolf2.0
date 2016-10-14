using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPersonagem : MonoBehaviour
{
    public static SelectPersonagem personagem;

    public MusicaManager musica;

    public GameObject[] player;

    public GameObject[] atributos;
    public GameObject[] atributosDesliga;
    public GameObject loading;
    public GameObject[] pos;

    public Animator[] animFoto, animFotoImg, animFotoText;
    
    public int select;
    public int select2 = 1;

    public string cena;

    bool podeP1 = true;
    bool podeP2 = true;
    bool podeDpad = true;
    bool podeDpad2 = true;
    bool startP1 = false;
    bool startP2 = false;

    void Awake()
    {
        personagem = this;
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("Players") == 1)
        {
            player[0].SetActive(true);
            player[1].SetActive(false);
            select = 0;
            select2 = 999;
        }
        else if (PlayerPrefs.GetInt("Players") == 2)
        {
            player[0].SetActive(true);
            player[1].SetActive(true);
            select2 = 1;
        }

        for(int i = 0; i < atributos.Length; i++)
        {
            atributos[i].SetActive(false);
            atributosDesliga[i].SetActive(true);
        }
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("Players") == 2)
        {
            if(startP1 && startP2)
            {
                loading.SetActive(true);
                musica.Mudo();
                if (select == 0)
                {
                    PlayerPrefs.SetInt("Escolha", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Escolha", 2);
                }
                SceneManager.LoadScene(cena);
            }
        }
        else if(startP1)
        {
            loading.SetActive(true);
            musica.Mudo();
            if (select == 0)
            {
                PlayerPrefs.SetInt("Escolha", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Escolha", 2);
            }
            SceneManager.LoadScene(cena);
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.H))
        {
            startP1 = true;
        }
        
        if(Input.GetKeyDown(KeyCode.Joystick2Button7))
        {
            startP2 = true;
        }

        if (!startP1)
        {
            if (podeP1)
            {
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP1") > 0 || Input.GetKeyDown(KeyCode.JoystickButton5))
                {
                    Muda(true);
                }
				else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP1") < 0 || Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    Muda(false);
                }
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                atributos[select].SetActive(false);
                animFoto[select].SetTrigger("Entra");
                animFotoImg[select].SetTrigger("Entra");
                animFotoText[select].SetTrigger("Entra");
                atributosDesliga[select].SetActive(true);
                podeP1 = true;
            }
        }

        if (!startP2)
        {
            if (PlayerPrefs.GetInt("Players") == 2)
            {
                if (podeP2)
                {
                    if (Input.GetAxisRaw("DpadXP2") > 0)
                    {
                        Muda2(true);
                    }
                    else if (Input.GetAxisRaw("DpadXP2") < 0)
                    {
                        Muda2(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                    {
                        Select2();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick2Button1))
                {
                    atributos[select2].SetActive(false);
                    animFoto[select2].SetTrigger("Entra");
                    animFotoImg[select2].SetTrigger("Entra");
                    animFotoText[select2].SetTrigger("Entra");
                    atributosDesliga[select2].SetActive(true);
                    podeP2 = true;
                }
            }
        }

        if(Input.GetAxisRaw("DpadXP1") == 0)
        {
            podeDpad = true;
        }

        if (Input.GetAxisRaw("DpadXP2") == 0)
        {
            podeDpad2 = true;
        }
    }

    void Select2()
    {
        if (select2 == 0)
        {
            atributos[select2].SetActive(true);
            animFoto[select2].SetTrigger("Sai");
            animFotoImg[select2].SetTrigger("Sai");
            animFotoText[select2].SetTrigger("Sai");
            atributosDesliga[select2].SetActive(false);
            podeP2 = false;
            return;
        }
        else if (select2 == 1)
        {
            atributos[select2].SetActive(true);
            animFoto[select2].SetTrigger("Sai");
            animFotoImg[select2].SetTrigger("Sai");
            animFotoText[select2].SetTrigger("Sai");
            atributosDesliga[select2].SetActive(false);
            podeP2 = false;
            return;
        }
    }

    void Select()
    {
        if (select == 0)
        {
            atributos[select].SetActive(true);
            animFoto[select].SetTrigger("Sai");
            animFotoImg[select].SetTrigger("Sai");
            animFotoText[select].SetTrigger("Sai");
            atributosDesliga[select].SetActive(false);
            podeP1 = false;
            return;
        }
        else if (select == 1)
        {
            atributos[select].SetActive(true);
            animFoto[select].SetTrigger("Sai");
            animFotoImg[select].SetTrigger("Sai");
            animFotoText[select].SetTrigger("Sai");
            atributosDesliga[select].SetActive(false);
            podeP1 = false;
            return;
        }
    }

    void Muda2(bool positivo)
    {
        if (podeDpad2)
        {
            if (positivo)
            {
                select2++;
                if (select2 == select || select2 > 3 && select == 0)
                {
                    if (select == 3)
                    {
                        select2 = 0;
                    }
                    else if (select == 0)
                    {
                        select2 = 1;
                    }
                    else
                    {
                        select2++;
                    }
                }
                if (select2 > 3)
                {
                    select2 = 0;
                }
            }
            else
            {
                select2--;
                if (select2 == select)
                {
                    if (select == 3)
                    {
                        select2 = 0;
                    }
                    else if (select == 0)
                    {
                        select2 = 3;
                    }
                    else
                    {
                        select2--;
                    }
                }
                if (select2 < 0)
                {
                    select2 = 3;
                }
            }
        }

        player[1].transform.position = pos[select2].transform.position;
        /*
        if (select == 0 && select2 != 1)
        {
            player[1].enabled = true;
            player[0].enabled = false;
            select = 1;
        }
        else if (select == 1 && select2 != 0)
        {
            player[0].enabled = true;
            player[1].enabled = false;
            select = 0;
        }*/
        podeDpad2 = false;
    }

    void Muda(bool positivo)
    {
        if (podeDpad)
        {
            if (positivo)
            {
                select++;
                if (select == select2 || select > 3 && select2 == 0)
                {
                    if (select2 == 3)
                    {
                        select = 0;
                    }
                    else if (select2 == 0)
                    {
                        select = 1;
                    }
                    else
                    {
                        select++;
                    }
                }
                if (select > 3)
                {
                    select = 0;
                }
            }
            else
            {
                select--;
                if(select == select2)
                {
                    if(select2 == 3)
                    {
                        select = 0;
                    }
                    else if (select2 == 0)
                    {
                        select = 3;
                    }
                    else
                    {
                        select--;
                    }
                }
                if(select < 0)
                {
                    select = 3;
                }
            }
        }

        player[0].transform.position = pos[select].transform.position;
            /*
            if (select == 0 && select2 != 1)
            {
                player[1].enabled = true;
                player[0].enabled = false;
                select = 1;
            }
            else if (select == 1 && select2 != 0)
            {
                player[0].enabled = true;
                player[1].enabled = false;
                select = 0;
            }*/
            podeDpad = false;
        }
}
