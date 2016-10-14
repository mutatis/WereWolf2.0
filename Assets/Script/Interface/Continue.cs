using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    public Text text;

    public GameObject canvas;

    public ManagerStartGame manager;

    int tempo = 11;

    void Start()
    {
        StartCoroutine("Time");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Respawn();    
        }
    }

    void Respawn()
    {
        manager.Comeca();
        canvas.SetActive(false);
    }

    IEnumerator Time()
    {
        tempo--;
        text.text = tempo.ToString();
        if (tempo <= 0)
        {
            SceneManager.LoadScene("Dead");
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine("Time");
        }
    }

}
