using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliAumenDimi : MonoBehaviour
{
    public SelectEnemySlam slam;

    public GameObject sli;

    public float maxSli;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick2Button0) && transform.localScale.x < maxSli)
        {
            Manager.manager.player[0].GetComponent<PlayerStats>().playerAnim.SetTrigger("JogaSlam");
            Manager.manager.player[1].GetComponent<PlayerStats>().playerAnim.SetTrigger("JogaSlam");
            Manager.manager.player[1].GetComponent<Rigidbody>().velocity = new Vector3(0, 35, 0);
            slam.enabled = true;
            sli.SetActive(false);
        }
        if(transform.localScale.x <= 0.05f)
        {
            for (int i = 0; i < Manager.manager.enemy.Length; i++)
            {
                Manager.manager.enemy[i].GetComponent<EnemyController>().enabled = true;
            }
            for (int i = 0; i < Manager.manager.player.Length; i++)
            {
                Manager.manager.player[i].GetComponent<PlayerStats>().playerAnim.SetBool("SlamDunk", false);
            }
            sli.SetActive(false);
        }

        transform.localScale = new Vector3(transform.localScale.x - 0.004f, transform.localScale.y - 0.004f, transform.localScale.z - 0.004f);
    }
}