using UnityEngine;
using System.Collections;

public class SlamAreaCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyRanged")
        {
            other.gameObject.GetComponent<EnemyController>().Dano(500, false, gameObject);
        }
    }
}
