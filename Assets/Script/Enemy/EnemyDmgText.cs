using UnityEngine;
using System.Collections;

public class EnemyDmgText : MonoBehaviour
{
    public EnemyController enemy;

    public EnemyRanged enemyR;

	public BossController boss;

    public int tipo;

	void Update ()
    {
        if (tipo == 0)
        {
            if ((enemy.transform.localScale.x < 0 && transform.localScale.x > 0) || (enemy.transform.localScale.x > 0 && transform.localScale.x < 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
        }

        if (tipo == 1)
        {
            if ((enemyR.transform.localScale.x < 0 && transform.localScale.x > 0) || (enemyR.transform.localScale.x > 0 && transform.localScale.x < 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
        }

        if (tipo == 2)
        {
			if ((boss.transform.localScale.x < 0 && transform.localScale.x > 0) || (boss.transform.localScale.x > 0 && transform.localScale.x < 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}