using UnityEngine;
using System.Collections;

public class ManagerStartGame : MonoBehaviour
{
    public GameObject andarilho, presas, parede, continueGame;

    GameObject temp;

    Vector3 cameraPos;

    void Start()
    {
        cameraPos = Camera.main.transform.position;
        Comeca();
    }

    public void Comeca()
    {
        parede.SetActive(false);
        Camera.main.transform.position = cameraPos;

        if (PlayerPrefs.GetInt("Players") <= 1)
        {
            if (PlayerPrefs.GetInt("Escolha") == 1)
            {
                Instantiate(andarilho, new Vector3(-25, 2, 0), transform.rotation);
            }
            else if (PlayerPrefs.GetInt("Escolha") == 2)
            {
                Instantiate(presas, new Vector3(-25, 2, 0), transform.rotation);
            }
        }
        else
        {
            Instantiate(andarilho, new Vector3(-25, 0, 0), transform.rotation);
            temp = Instantiate(presas, new Vector3(-25, 0, 0), transform.rotation) as GameObject;
            temp.GetComponent<PlayerStats>().player = Player.Player2;
            temp.GetComponent<PlayerDonsAndarilho>().player = PlayerDom.Player2;
        }
    }
}