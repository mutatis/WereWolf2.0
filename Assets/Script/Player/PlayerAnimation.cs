using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public PlayerMovment playerMov;
    public PlayerAttackController playerAttack;
    public PlayerDano playerDano;
    public PlayerStats playerStats;

    public Animator anim;

    [FMODUnity.EventRef]
    public string socoFraco;

    [FMODUnity.EventRef]
    public string socoCrinos;

    [FMODUnity.EventRef]
    public string miss, missForte;

    [FMODUnity.EventRef]
    public string crit;

    [FMODUnity.EventRef]
    public string queda;

    [HideInInspector]
    public FMOD.Studio.EventInstance audioInstance;

   [FMODUnity.EventRef]
    public string socoForte;

    public bool levanta;

    GameObject obj;
    public GameObject[] x;

    public GameObject pack;

    bool idle = true;
    bool run;

    void Start()
    {
        x = GameObject.FindGameObjectsWithTag("Manager");
    }

    public void EndSlam()
    {
        playerAttack.pode = false;
    }

    public void TrueSlam()
    {
        playerAttack.pode = true;
    }

    public void StartSlam()
    {
        pack.SetActive(true);
        pack.GetComponent<PackAtiva>().qual = 2;
    }

    public void JumpStart()
    {
        playerMov.JumpUp();
    }

    public void CaboPulo()
    {
        playerMov.CaboPulo();
    }
    
    public void GrabIdle()
    {
        playerAttack.enemy.GetComponent<EnemyController>().head.enabled = false;
        playerAttack.enemy.GetComponent<EnemyController>().anim.gameObject.SetActive(false);
    }

    public void GrabWalk()
	{
		playerAttack.enemy.GetComponent<EnemyController>().head.enabled = true;
		playerAttack.enemy.GetComponent<EnemyController>().anim.gameObject.SetActive(true);
    }

    public void Queda()
    {
        playerDano.caindo = false;
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(queda);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();
    }

    void NotIdle()
    {
        idle = false;
    }

    void OkIdle()
    {
        idle = true;
    }

    public void Levanta()
    {
        levanta = false;
    }

    public void SocoForte()
    {
        playerAttack.ataque = 0.12f;
        playerAttack.mov = true;
    }

    public void Cai()
    {
        levanta = true;
    }

    public void Stun()
    {
        playerDano.stun = false;
        anim.SetBool("Stun", false);
        OkIdle();
    }

    public void Miss()
    {
        if (!playerStats.crinos)
        {
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(miss);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(socoCrinos);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
    }

    public void MissForte()
    {
        if (!playerStats.crinos)
        {
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(missForte);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(socoCrinos);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
    }

    public void Atacando()
    {
        obj = null;
        playerMov.isMov = true;
        playerAttack.mov = true;
    }

    public void Liberated()
    {
        levanta = false;
        playerAttack.obj = obj;
        playerAttack.Libero();
        playerAttack.mov = false;
        playerMov.isMov = false;
        playerMov.transform.position = new Vector3(transform.position.x, playerMov.transform.position.y, playerMov.transform.position.z);
        OkIdle();
    }

    public void Dead()
    {
        if (playerStatus.vidas < 2)
        {
            x[0].GetComponent<ManagerStartGame>().continueGame.SetActive(true);
            Destroy(playerStats.gameObject);
        }
        else
        {
            playerStatus.life = playerStatus.lifeMax;
            playerStatus.vidas--;
        }
    }

    public void Ataca()
    {
        NotIdle();
    }

    void Dano(GameObject other)
    {
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(socoFraco);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();

        int x = Random.Range(0, 100);
        if(x <= playerStatus.critChance)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            playerStats.rage += playerStatus.rageRegen;
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(crit);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            playerStats.rage += playerStatus.rageRegen;
        }
        StartCoroutine(Vibrar());
    }

    public void SlamDmg(GameObject other)
    {
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(socoForte);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();
        int x = Random.Range(0, 100);
        if (x <= playerStatus.critChance)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerMov.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerMov.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerMov.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerMov.gameObject, playerStatus.knockback);
            }
            playerStats.rage += playerStatus.rageRegen;
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(crit);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerMov.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerMov.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerMov.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerMov.gameObject, playerStatus.knockback);
            }
            playerStats.rage += playerStatus.rageRegen;
        }
        StartCoroutine(Vibrar());
    }

    IEnumerator Vibrar()
    {
        GamePad.SetVibration(0, 0.15f, 0.15f);
        yield return new WaitForSeconds(0.1f);
        GamePad.SetVibration(0, 0, 0);
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano && 
                (anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte") || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") ||
                playerAttack.attackComboNum >= 4) || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa1") || 
                anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa2"))
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
            if (other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano && playerAttack.attackComboNum < 4)
            {
                obj = other.gameObject;
                Dano(obj);
            }
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            if (other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano && 
                (anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte") || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") || 
                playerAttack.attackComboNum >= 4) || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa1") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa2"))
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
            if (other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
        }
        else if (other.gameObject.tag == "SubBoss")
        {
            if (other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano && 
                (anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte") || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") || 
                playerAttack.attackComboNum >= 4) || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa1") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa2"))
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
            if (other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            if (other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano && 
                (anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte") || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") || 
                playerAttack.attackComboNum >= 4) || anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa1") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa2"))
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
            if (other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
        }
    }
}