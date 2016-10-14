using UnityEngine;
using System.Collections;

public class EletrocuteDon : MonoBehaviour
{
    public float dmg;

    [FMODUnity.EventRef]
    public string raio;

    FMOD.Studio.EventInstance audioInstance;

    GameObject obj;

    void Start()
	{
		audioInstance = FMODUnity.RuntimeManager.CreateInstance(raio);
		audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
		audioInstance.start();
        StartCoroutine("GO");
    }

    IEnumerator GO()
    {
		yield return new WaitForSeconds(1);
		audioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        Destroy(gameObject);
    }

    void Dano(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
       	{
        	other.gameObject.GetComponent<EnemyController>().Dano(dmg, false, gameObject);
        }
        else if (other.gameObject.tag == "EnemyRanged")
       	{
        	other.gameObject.GetComponent<EnemyRanged>().Dano(dmg, false, gameObject);
		}
		else if (other.gameObject.tag == "Boss")
		{
			other.gameObject.GetComponent<BossController>().Dano(dmg, false, gameObject);
		}        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            if (other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
		}
		else if (other.gameObject.tag == "Boss")
		{
			if (other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
			{
				obj = other.gameObject;
				Dano(obj);
			}
		}
    }
}
