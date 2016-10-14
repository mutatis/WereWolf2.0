using UnityEngine;
using System.Collections;

public class Pacman : MonoBehaviour
{
    public Animator anim;

    public float dmg;

    public GameObject obj;

    [FMODUnity.EventRef]
    public string inicio, dano, fim;

    FMOD.Studio.EventInstance vol;
    FMOD.Studio.EventInstance volInicio;

    public float vel;

    int contador, temp, mato;

    void Start()
    {
        if(obj.GetComponent<PlayerStats>().transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            vel *= -1;
        }
        volInicio = FMODUnity.RuntimeManager.CreateInstance(inicio);
        volInicio.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        volInicio.start();
    }

    void Update()
    {
		if (Time.timeScale != 0) 
		{
			if (contador == 0)
				transform.Translate (vel, 0, 0, Space.World);
	        
			if (temp == 2) 
			{
				volInicio.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
				vol = FMODUnity.RuntimeManager.CreateInstance (fim);
				vol.setVolume (PlayerPrefs.GetFloat ("VolumeFX"));
				vol.start ();
				Destroy (gameObject);
			}
		}
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(1);
        transform.position = new Vector3(transform.position.x, obj.transform.position.y, obj.transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        vel *= -1;
        contador = 0;
    }

    void Dano(GameObject other)
    {
        mato++;
        anim.SetInteger("Mato", mato);
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Dano(dmg, false, gameObject);
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
			other.gameObject.GetComponent<EnemyRanged>().Dano(dmg, false, gameObject);;
        }
        else if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<BossController>().Dano(dmg, false, gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Parede3")
        {
            temp++;
            contador = 1;
            StartCoroutine("GO");
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                vol = FMODUnity.RuntimeManager.CreateInstance(dano);
                vol.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                vol.start();
                Dano(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            if (other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                vol = FMODUnity.RuntimeManager.CreateInstance(dano);
                vol.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                vol.start();
                Dano(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            if (other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                vol = FMODUnity.RuntimeManager.CreateInstance(dano);
                vol.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                vol.start();
                Dano(other.gameObject);
            }
        }
    }
}