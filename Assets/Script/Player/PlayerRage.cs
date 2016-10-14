using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour
{
    public GameObject[] obj;

    GameObject temp;

    public int tipo;

    public GameObject[] lua;

    float rageMaxTemp, rage;

    int x = 0;

    void FixedUpdate()
    {
        if (obj.Length <= 0 || obj[0] == null)
        {
            Procura();
        }
        else
        {
            rageMaxTemp = temp.GetComponent<PlayerStats>().playerStatus.rageMax;
        }

        rage = temp.GetComponent<PlayerStats>().rage;

        if (x == 0)
        {
            if (((rageMaxTemp / 6) * 1) > rage)
            {
                lua[1].SetActive(true);
                x = 1;
            }
        }
        else if (x == 1)
        {
            if ((rageMaxTemp / 6) * 2 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 1 < rage)
            {
                lua[1].GetComponent<Animator>().SetBool("Normal", true);
                lua[2].SetActive(true);
                x = 2;
            }
        }
        else if (x == 2)
        {
            if ((rageMaxTemp / 6) * 3 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 2 < rage)
            {
                lua[2].GetComponent<Animator>().SetBool("Normal", true);
                lua[3].SetActive(true);
                x = 3;
            }
        }
        else if (x == 3)
        {
            if ((rageMaxTemp / 6) * 4 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 3 < rage)
            {
                lua[3].GetComponent<Animator>().SetBool("Normal", true);
                lua[4].SetActive(true);
                x = 4;
            }
        }
        else if (x == 4)
        {
            if ((rageMaxTemp / 6) * 5 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 4 < rage)
            {
                lua[5].SetActive(true);
            }
            else
            {
                lua[5].GetComponent<Animator>().SetBool("Normal", true);
            }
        }
    }

    void Procura()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < obj.Length; i++)
        {
            switch (tipo)
            {
                case 1:
                    if (obj[i].GetComponent<PlayerStats>().player == Player.Player1)
                    {
                        temp = obj[i];
                    }
                    break;
                case 2:
                    if (obj[i].GetComponent<PlayerStats>().player == Player.Player2)
                    {
                        temp = obj[i];
                    }
                    break;
            }
        }
    }
}