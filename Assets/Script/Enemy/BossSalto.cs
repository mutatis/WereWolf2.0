using UnityEngine;
using System.Collections;

public class BossSalto : MonoBehaviour
{
    public float dmg;

    public Animator anim;

    public GameObject garra;

    IEnumerator GO()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        garra.SetActive(false);
        Time.timeScale = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            garra.SetActive(true);
            anim.SetTrigger("SaltoAcerto");
            other.gameObject.GetComponent<PlayerDano>().Dano(dmg, gameObject, false, true);
            StartCoroutine("GO");
        }
    }
}
