using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public GameObject[] obj;

    GameObject temp;

    public int tipo;
    
    public Slider sli;

    void Update()
    {
        if (obj.Length <= 0 || obj[0] == null)
        {
            Procura();
        }
        else
        {
            sli.maxValue = temp.GetComponent<PlayerStats>().playerStatus.lifeMax;
            sli.value = temp.GetComponent<PlayerStats>().playerStatus.life;
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

        sli.maxValue = temp.GetComponent<PlayerStats>().playerStatus.lifeMax;
    }
}