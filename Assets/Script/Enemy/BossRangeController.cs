using UnityEngine;
using System.Collections;

public class BossRangeController : MonoBehaviour
{
    public BossController longe;

    public Animator anim;

    public float dist1;
    public float dist2;

    public GameObject[] player;

    bool denovo = true;

    void Update()
    {
        player = Manager.manager.player;

		if (player.Length > 0) 
		{
			dist1 = Vector3.Distance (player [0].transform.position, transform.position);
		}

        if (PlayerPrefs.GetInt("Players") > 1)
            dist2 = Vector3.Distance(player[1].transform.position, transform.position);

        if (!longe.isAttack)
        {
            if (dist1 > 4 || dist2 > 4)
            {
                longe.perto = false;
            }
            else if (denovo)
            {
                longe.perto = true;

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle") && longe.life > 0)
                    anim.SetTrigger("Idle");

                if (dist1 < 3.5f)
                    longe.player = player[0];
                if (dist2 < 3.5f && PlayerPrefs.GetInt("Players") > 1)
                    longe.player = player[1];

                StartCoroutine(GO());
            }
        }
    }

    IEnumerator GO()
    {
        denovo = false;
        yield return new WaitForSeconds(2);
        denovo = true;
    }
}