using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerDano : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;
    public PlayerAttackController playerAttack;
    public Agarra agarra;
    
    public bool stun, caindo;

    [FMODUnity.EventRef]
    public string blockSound;
    [FMODUnity.EventRef]
    public string socoFracoEnemy, facaHit;

    FMOD.Studio.EventInstance audioInstanceCreator;

    public GameObject pegador;

    public int slamCont;

    void Update()
    {
        if(playerAttack.presa && playerStatus.life <= 0 && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDeadAndarilho"))
        {
            pegador.GetComponent<EnemyController>().Solta();            
            stun = true;
            playerAnim.anim.SetTrigger("Dead");
        }

        if(caindo)
        {
            if(transform.localScale.x > 0)
            {
                playerMov.x = -0.1f;
            }
            else
            {
                playerMov.x = 0.1f;
            }
        }
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(4);
        slamCont = 0;
    }
    
    public void Dano(float dmg, GameObject obj, bool faca = false, bool cai = false)
    {
        StopCoroutine("GO");
        StartCoroutine("GO");
        if (playerStatus.life > 0 && !playerAnim.levanta)
        {
            if(playerMov.isGrab)
			{
                agarra.End();
            }

			for (int i = 0; i < Manager.manager.enemy.Length; i++) 
			{
				Manager.manager.enemy[i].GetComponent<EnemyController>().anim.SetBool("Preso", false);
			}

            if (!playerAttack.presa)
            {
                if (!playerMov.jump)
                {
                    if ((slamCont > 2 && !playerAttack.block && !playerStats.crinos) || cai == true)
                    {
                        if (!faca)
                        {
                            audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(socoFracoEnemy);
                            audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                            audioInstanceCreator.start();
                        }
                        else
                        {
                            audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(facaHit);
                            audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                            audioInstanceCreator.start();
                        }
                        if ((obj.transform.position.x < transform.position.x && transform.localScale.x > 0) || (obj.transform.position.x > transform.position.x && transform.localScale.x < 0))
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                        playerAnim.anim.SetBool("Stun", true);
                        stun = true;
                        caindo = true;
                        playerAnim.anim.SetTrigger("Slam");
                        slamCont = 0;
                    }
                    else
                    {
                        if(slamCont > 2)
                        {
                            slamCont = 0;
                        }
                        if (!faca)
                        {
                            if (playerAttack.block)
                            {
                                audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(blockSound);
                                audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                                audioInstanceCreator.start();
                            }
                            else
                            {
                                audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(socoFracoEnemy);
                                audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                                audioInstanceCreator.start();
                            }
                        }
                        else
                        {
                            audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(facaHit);
                            audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                            audioInstanceCreator.start();
                        }
                        if ((obj.transform.position.x > transform.position.x && transform.localScale.x > 0) || obj.transform.position.x < transform.position.x && transform.localScale.x < 0)
                        {
                            playerAnim.anim.SetBool("Costas", false);
                        }
						else //if ((obj.transform.position.x < transform.position.x && transform.localScale.x < 0) || obj.transform.position.x > transform.position.x && transform.localScale.x < 0)
                        {
                            playerAnim.anim.SetBool("Costas", true);
                        }
                        playerAnim.anim.SetBool("Stun", true);
                        stun = true;
                        playerAnim.anim.SetTrigger("Dano");
                    }
                }
            }
            else
            {
                audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(socoFracoEnemy);
                audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                audioInstanceCreator.start();
                pegador.GetComponent<EnemyController>().anim.SetTrigger("Dano");
            }
            slamCont++;
            dmg -= playerStatus.dmgTrash;
            if(playerAttack.block)
            {
                dmg *= playerStatus.blockEffect;
            }
            playerStatus.life -= dmg;
            playerStats.rage += playerStatus.rageRegen;
            if (playerStatus.life <= 0)
            {
                if(playerAttack.presa)
                {
                    pegador.GetComponent<EnemyController>().Solta();
                }
                if ((obj.transform.position.x < transform.position.x && transform.localScale.x > 0) || (obj.transform.position.x > transform.position.x && transform.localScale.x < 0))
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                stun = true;
                if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDeadAndarilho"))
                {
                    playerAnim.anim.SetTrigger("Dead");
                }
            }
            //StartCoroutine(Vibrar());
        }
    }

    IEnumerator Vibrar()
    {
        GamePad.SetVibration(0, 1f, 1f);
        yield return new WaitForSeconds(0.2f);
        GamePad.SetVibration(0, 0, 0);
	}
}