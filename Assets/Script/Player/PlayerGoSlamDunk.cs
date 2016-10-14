using UnityEngine;
using System.Collections;

public class PlayerGoSlamDunk : MonoBehaviour
{
    [HideInInspector]
    public GameObject obj;

    [HideInInspector]
    public int selecionado;

    Vector3 direction;

    float dist;

    bool temp;

    void Update()
    {
        if (obj != null)
        {
            dist = Vector3.Distance(obj.transform.position, transform.position);
        }

        if (dist > 2f)
        {
            direction = obj.transform.position - transform.position;
            direction.Normalize();
            transform.Translate((direction.x), (direction.y / 2), direction.z / 4);
        }
        else if(!temp)
        {
            StartCoroutine("GO");
        }
    }

    IEnumerator GO()
    {
        temp = true;
        obj.GetComponent<SlamArea>().Matador();
        Time.timeScale = 0.1f;
        gameObject.GetComponent<PlayerDonsAndarilho>().FlashSlam();
        gameObject.GetComponent<PlayerStats>().playerAnim.SetTrigger("SlamBateu");
        yield return new WaitForSeconds(0.07f);
        gameObject.GetComponent<PlayerStats>().playerAnim.SetTrigger("Akuma");
        yield return new WaitForSeconds(0.08f);
        for (int i = 0; i < Manager.manager.player.Length; i++)
        {
            Manager.manager.player[i].GetComponent<PlayerStats>().playerAnim.SetBool("SlamDunk", false);
        }
        Time.timeScale = 1;
        Destroy(obj);
        Destroy(gameObject.GetComponent<PlayerGoSlamDunk>());
    }
}
