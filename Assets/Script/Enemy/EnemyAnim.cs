using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour
{
    public EnemyController controller;

    public EnemyRanged controller2;

    public SubBossController subBoss;

    public BossController boss;

    public Animator headAnim, faca;

    public GameObject obj;

    public int tipo;

    public float dmg;

    FMOD.Studio.EventInstance fomorSound;

    [FMODUnity.EventRef]
    public string fomorDescida, fomorSubida;

    [HideInInspector]
    public string nome;

    public void FomorSobe()
    {
        fomorSound = FMODUnity.RuntimeManager.CreateInstance(fomorSubida);
        fomorSound.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        fomorSound.start();
    }

    public void FomorDesce()
    {
        fomorSound = FMODUnity.RuntimeManager.CreateInstance(fomorDescida);
        fomorSound.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        fomorSound.start();
    }

    public void Levantando()
    {
        if(tipo == 1)
        {
            controller2.caindo = false;
        }
        else if(tipo == 0)
        {
            controller.caindoSlam = false;
        }
    }

    public void NotIdle()
    {
        faca.SetTrigger("NotIdle");
    }

    public void IdleRanged()
    {
        faca.SetTrigger("Idle");
    }

    public void Apanha()
    {
        if (tipo == 1)
        {
            controller2.Apanha();
        }
        else if (tipo == 3)
        {
            boss.Apanha();
        }
        else
        {
            controller.Apanha();
        }
    }
    
    public void PodeSalta()
    {
        boss.PodeSalta();
    }

    public void Run()
    {
        headAnim.SetTrigger("Run");
    }

    public void Desliga()
    {
        gameObject.SetActive(false);
    }

    public void Liberated()
    {
        if (tipo == 1)
        {
            controller2.combate = true;
        }
        else if(tipo == 0)
        {
            controller.combate = true;
        }
    }

    public void Tiro()
    {
        controller2.Atira();
    }

    public void Return()
    {
        if (tipo == 1)
        {
            controller2.stun = false;
        }
        if (tipo == 2)
        {
            subBoss.stun = false;
        }
        if(tipo == 3)
        {
            boss.stun = false;
        }
        else
        {
            if (controller != null)
            {
                controller.stun = false;
                controller.slam = false;
                controller.anim.SetBool("isSlam", false);
                controller.transform.position = new Vector3(transform.position.x, controller.transform.position.y, controller.transform.position.z);
            }
        }
    }

    public void DanoAgain()
    {
        if (tipo == 1)
        {
            controller2.DanoAgain();
        }
        if(tipo == 2)
        {
            subBoss.DanoAgain();
        }
        if(tipo == 3)
        {
            boss.DanoAgain();
        }
        else
        {
            if (controller != null)
            {
                controller.DanoAgain();
            }
        }
    }

    public void Dead()
    {
        FollowTarget.follow.quant--;
        if (tipo == 0)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + controller.xp));
        }
        if (tipo == 1)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + controller2.xp));
        }
        if(tipo == 2)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + subBoss.xp));
        }
        if (tipo == 3)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + boss.xp));
        }
        Destroy(obj);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerDano>().Dano(dmg, gameObject);         
        }
        if (other.gameObject.tag == "Parede")
        {
            if (tipo == 1)
            {
                if (controller2.roamming)
                {
                    controller2.Wait();
                }
            }
            else
            {
                if (controller.roamming)
                {
                    controller.StartCoroutine("Wait");
                }
            }
        }
    }
}