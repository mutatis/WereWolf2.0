using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string sceneName;

    public int tipo;

	void Start ()
    {
        if (tipo == 0)
        {
            StartCoroutine("GO");
        }
	}

    public void Muda()
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(2);
        Muda();
    }
}
