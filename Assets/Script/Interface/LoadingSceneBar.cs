using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class LoadingSceneBar : MonoBehaviour
{
    public GameObject congrat;

    public PacmanGame pacman;

    public string cena;

    GameObject[] obj;

    /*public Text text;

    void Start()
    {
        StartCoroutine(AsynchronousLoad(cena));
    }*/

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Pozinho");
    }

    void Update()
    {
        obj = GameObject.FindGameObjectsWithTag("Pozinho");

        if(obj.Length <= 0)
        {
            congrat.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return))
        {
            pacman.ParaSom();
            SceneManager.LoadScene(cena);
        }
    }

    /*IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            text.text = ("Loading progress: " + (progress * 100) + "%");
                //progress * 100).ToString();
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                Debug.Log("Press a key to start");
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }*/
}
