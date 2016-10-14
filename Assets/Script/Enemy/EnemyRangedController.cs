using UnityEngine;
using System.Collections;

public class EnemyRangedController : MonoBehaviour
{
    public EnemyRanged longe;

    public float dist1;
    public float dist2;

    bool podeCombate;

    public GameObject[] player;

    void Update()
    {
        player = Manager.manager.player;

        dist1 = Vector3.Distance(player[0].transform.position, transform.position);
        if(PlayerPrefs.GetInt("Players") > 1)
        dist2 = Vector3.Distance(player[1].transform.position, transform.position);

        if (dist1 > 3f || dist2 > 3f)
        {
            longe.perto = false;
            podeCombate = false;
        }
        else
        {
            if (!podeCombate)
            {
                longe.combate = true;
                podeCombate = true;
            }
            longe.perto = true;

            if (!longe.anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle") && longe.life > 0)
                longe.anim.SetTrigger("Idle");

            if (dist1 < 1f)
                longe.player = player[0];
            if (dist2 < 1f && PlayerPrefs.GetInt("Players") > 1)
                longe.player = player[1];
        }
    }
}