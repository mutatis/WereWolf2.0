using UnityEngine;
using System.Collections;

public class PackAtiva : MonoBehaviour
{
    public ArrancarPelos arranca;

    public GameObject slam;

    public SpriteRenderer[] sprt;

    public Sprite[] img;

    public int temp;
    [HideInInspector]
    public int qual;

    bool pode;

    int x = 0;
    int y = 10;
    
    void Update()
    {
        switch (x)
        {
            case 1:
                sprt[0].sprite = img[0];
                sprt[1].sprite = null;
                if(Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space))
                {
                    Acerto(0);
                    x = 10;
                }
                else if(Input.anyKeyDown)
                {
                    Erro();
                }
                break;
            case 2:
                sprt[0].sprite = img[1];
                sprt[1].sprite = null;
                if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.C))
                {
                    Acerto(0);
                    x = 10;
                }
                else if (Input.anyKeyDown)
                {
                    Erro();
                }
                break;
            case 3:
                sprt[0].sprite = img[2];
                sprt[1].sprite = null;
                if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Z))
                {
                    Acerto(0);
                    x = 10;
                }
                else if (Input.anyKeyDown)
                {
                    Erro();
                }
                break;
            case 4:
                sprt[0].sprite = img[3];
                sprt[1].sprite = null;
                if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.X))
                {
                    Acerto(0);
                    x = 10;
                }
                else if (Input.anyKeyDown)
                {
                    Erro();
                }
                break;
        }
        switch(y)
        {
            case -1:
                sprt[1].sprite = img[0];
                sprt[0].sprite = null;
                if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                {
                    Acerto(1);
                    y = 10;
                }
                else if (Input.anyKeyDown)
                {
                    Erro();
                }
                break;
            case -2:
                sprt[1].sprite = img[1];
                sprt[0].sprite = null;
                if (Input.GetKeyDown(KeyCode.Joystick2Button1))
                {
                    Acerto(1);
                    y = 10;
                }
                else if (Input.anyKeyDown)
                {
                    Erro();
                }
                break;
            case -3:
                sprt[1].sprite = img[2];
                sprt[0].sprite = null;
                if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                {
                    Acerto(1);
                    y = 10;
                }
                else if (Input.anyKeyDown)
                {
                    Erro();
                }
                break;
            case -4:
                sprt[1].sprite = img[3];
                sprt[0].sprite = null;
                if (Input.GetKeyDown(KeyCode.Joystick2Button3))
                {
                    Acerto(1);
                    y = 10;
                }
                else if (Input.anyKeyDown)
                {
                    Erro();
                }
                break;
        }

        if (temp >= 6 && qual == 1)
        {
            Arranca();
        }
        else if (temp >= 6 && qual == 2 && pode)
        {
            Slam();
        }
    }

    void Arranca()
    {
        arranca.enabled = true;
        ParaTudo();
        Erro();
    }

    void Slam()
    {
        Time.timeScale = 1;
        Manager.manager.player[0].GetComponent<PlayerStats>().playerAnim.SetTrigger("AcertoSlam");
        Manager.manager.player[1].GetComponent<PlayerStats>().playerAnim.SetTrigger("PuloSlam");
        Manager.manager.player[1].GetComponent<PlayerMovment>().acerto = true;
        Manager.manager.player[1].GetComponent<PlayerMovment>().completo = true;
        Instantiate(slam);
        ParaTudo();
        pode = false;
        Erro(true);
    }

    void Acerto(int player)
    {
        Manager.manager.player[1].GetComponent<PlayerStats>().playerAnim.SetBool("SlamDunk", true);
        Manager.manager.player[1].GetComponent<PlayerStats>().playerAnim.SetTrigger("StartSlam");
        if (temp < 6)
        {
            temp++;
        }
        sprt[player].sprite = img[4];
    }

    void Erro(bool erro = false)
    {
        Time.timeScale = 1;
        if (!erro)
        {
            Manager.manager.player[0].GetComponent<PlayerStats>().playerAnim.SetTrigger("AcertoSlam");
            Manager.manager.player[1].GetComponent<PlayerStats>().playerAnim.SetTrigger("PuloSlam");
            Manager.manager.player[1].GetComponent<PlayerMovment>().acerto = true;
            /*for (int i = 0; i < Manager.manager.player.Length; i++)
            {
                Manager.manager.player[i].GetComponent<PlayerStats>().playerAnim.SetBool("SlamDunk", false);
            }*/
        }
        for (int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            Manager.manager.enemy[i].GetComponent<EnemyController>().enabled = true;
        }
        StopCoroutine("GO");
        temp = 0;
        sprt[0].sprite = null;
        sprt[1].sprite = null;
        y = 10;
        x = 10;
        gameObject.SetActive(false);
    }

    void ParaTudo()
    {
        for (int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            Manager.manager.enemy[i].GetComponent<EnemyController>().enabled = false;
            Manager.manager.enemy[i].GetComponent<EnemyController>().StopAllCoroutines();
        }
    }

    IEnumerator GO()
    {
        ParaTudo();
        temp = 0;
        yield return new WaitForSeconds(0.1f);
        x = Random.Range(1, 4);
        yield return new WaitForSeconds(0.1f);
        if (y == 10)
        {
            y = Random.Range(-4, -1);
        }
        else
        {
            Erro();
        }
        yield return new WaitForSeconds(0.1f);
        if (x == 10)
        {
            x = Random.Range(1, 4);
        }
        else
        {
            Erro();
        }
        yield return new WaitForSeconds(0.1f);
        if (y == 10)
        {
            y = Random.Range(-4, -1);
        }
        else
        {
            Erro();
        }
        yield return new WaitForSeconds(0.1f);
        if (x == 10)
        {
            x = Random.Range(1, 4);
        }
        else
        {
            Erro();
        }
        yield return new WaitForSeconds(0.1f);
        if (y == 10)
        {
            y = Random.Range(-4, -1);
        }
        else
        {
            Erro();
        }
        StopCoroutine("GO");
        /*yield return new WaitForSeconds(1);
        if (y == 10 && x == 10)
        {
            y = Random.Range(-4, -1);
            x = Random.Range(1, 4);
        }
        else
        {
            Erro();
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Manager.manager.player[0].GetComponent<PlayerMovment>().isMov = true;
            Manager.manager.player[1].GetComponent<PlayerMovment>().isMov = true;
            Manager.manager.player[1].GetComponent<PlayerStats>().playerAnim.SetBool("SlamDunk", true);
            Manager.manager.player[1].GetComponent<PlayerStats>().playerAnim.SetTrigger("StartSlam");
            //Manager.manager.player[1].GetComponent<Rigidbody>().velocity = new Vector3(0, 13, 0);
            Time.timeScale = 0.1f;
            StartCoroutine("GO");
            pode = true;
        }
    }
}