using UnityEngine;
using System.Collections;

public class WallGraniteSobe : MonoBehaviour
{
    public int dano;

    void Slam(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Slam((dano * 2) + (dano * 0.25f), true, gameObject, 1);
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            other.gameObject.GetComponent<EnemyRanged>().Slam((dano * 2) + (dano * 0.25f), true, gameObject, 1);
        }
        else if (other.gameObject.tag == "SubBoss")
        {
            other.gameObject.GetComponent<SubBossController>().Slam((dano * 2) + (dano * 0.25f), true, gameObject, 1);
        }
        else if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<BossController>().Slam((dano * 2) + (dano * 0.25f), true, gameObject, 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyRanged" || 
            other.gameObject.tag == "SubBoss" || other.gameObject.tag == "Boss")
        {
            Slam(other.gameObject);
        }
    }
}
