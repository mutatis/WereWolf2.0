using UnityEngine;
using System.Collections;

public class SelectEnemySlam : MonoBehaviour
{
    public GameObject obj, area;

    float x;

    int selecionado;

    bool zero;

    void Start()
    {
        Instantiate(area);
        StartCoroutine("GO");
    }

    void Update()
    {
        if (Manager.manager.player[1].GetComponent<Rigidbody>().velocity.y < 0)
        {
            Manager.manager.player[0].GetComponent<PlayerStats>().playerAnim.SetTrigger("EsperandoSlam");
            Manager.manager.player[1].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            Aperto();
            Manager.manager.player[1].AddComponent<PlayerGoSlamDunk>();
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().selecionado = selecionado;
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().obj = GameObject.FindGameObjectWithTag("SlamArea");
            Destroy(gameObject);
        }
        
    }

    void Aperto()
    {
        for (int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            Manager.manager.enemy[i].GetComponent<EnemyController>().enabled = true;
        }
        //obj.SetActive(true);
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(2);
        Aperto();
    }
}