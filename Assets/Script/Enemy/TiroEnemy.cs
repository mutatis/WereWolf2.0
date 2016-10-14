using UnityEngine;
using System.Collections;

public class TiroEnemy : MonoBehaviour
{
    public GameObject obj;

    public Animator anim;

    [FMODUnity.EventRef]
    public string tiro;

    FMOD.Studio.EventInstance tiroInstance;

    public float velocityX, dmg, range;

    bool foi, acerto, subo, desce;

    void Start()
    {
        StartCoroutine("GO");
        tiroInstance = FMODUnity.RuntimeManager.CreateInstance(tiro);
        tiroInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        tiroInstance.start();
    }

    void Update()
    {
        if(obj != null && !foi)
        {
            if (obj.transform.localScale.x > 0)
            {
                velocityX *= -1;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;    
            }
            foi = true;
        }
        if (!acerto)
        {
            transform.Translate(new Vector3(velocityX, 0, 0), Space.World);
        }

        if(subo)
        {
            transform.Translate(new Vector3(0, 0.1f, 0), Space.World);
        }
        else if(desce)
        {
            transform.Translate(new Vector3(0, -0.13f, 0), Space.World);
        }
    }

    public void Para()
    {
        desce = false;
    }

    public void Subo()
    {
        subo = true;
    }

    public void Desce()
    {
        subo = false;
        desce = true;
    }

    public void Acabo()
    {
        Destroy(gameObject);
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerDano>().Dano(dmg, gameObject, true);
            acerto = true;
            anim.enabled = true;
        }
    }
}