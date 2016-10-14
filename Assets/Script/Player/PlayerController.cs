using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public static PlayerController playerController;

    public PlayerAnimation anim;

    //public Animator crinosAnim;
    //public Animator playerAnim;
    public Animator grabAnim;

    public Rigidbody rig;

    public PlayerContr player;
    /*
    [FMODUnity.EventRef]
    public string miss;
    [FMODUnity.EventRef]
    public string blockSound;
    [FMODUnity.EventRef]
    public string jumpSound;
    */
    FMOD.Studio.EventInstance audioInstanceCreator;

    [HideInInspector]//rage
    public float  gnose, z, x;
    [HideInInspector]
    public bool isAttack = true;
    [HideInInspector]
    public bool block;
    [HideInInspector]
    public bool isJump = true;
    [HideInInspector]
    public bool isGrab = false;
    [HideInInspector]
    public bool isRun = true;
    public bool jump, stun, crinos, apanha, presa;
    //public bool call, lunar;

    public int contador, engage, flooda, solta;

    public string nome;

    public GameObject enemy, pegador;

    bool r1, isIdle;

    int jumpAttack, dano;

    void Awake()
    {
        playerController = this;
    }

    void Start()
    {
        StartCoroutine("GnoseStart");
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (playerStatus.life <= 0)
            {
                if (enemy.transform.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                else if (enemy.transform.position.x > transform.position.x && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                anim.anim.SetTrigger("Dead");
                anim.anim.SetBool("isDead", true);
                if (anim.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDead"))
                {
                    gameObject.GetComponent<PlayerController>().enabled = false;
                }
            }

            if (x != 0 && !anim.anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !jump && !isGrab)
            {
                anim.anim.SetInteger("Vel", 1);
                anim.anim.SetTrigger("Run");
            }
            else if(z != 0 && !anim.anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !jump && !isGrab)
            {
                anim.anim.SetInteger("Vel", 1);
                anim.anim.SetTrigger("Run");
            }

            if (!anim.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !jump && !isGrab && z == 0 && x == 0 && !stun)
            {
                anim.anim.SetTrigger("Idle");
                anim.anim.SetInteger("Vel", 0);
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Space))
            {
                if (presa)
                {
                    solta++;
                }
            }

            if (!stun && playerStatus.life > 0)
            {
                if (x > 0 && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
                else if (x < 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
                switch (player)
                {
                    case PlayerContr.Player1:
                        if (isRun)
                        {
                            x = Input.GetAxis("HorizontalP1");
                            if (!jump)
                            {
                                z = Input.GetAxis("VerticalP1");
                            }
                            else
                            {
                                z = 0;
                            }
                            transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)));
                        }
                        else
                        {
                            x = 0;
                            z = 0;
                        }
                        
                        if(x == 0 && z == 0 && isGrab && !isIdle)
                        {
                            anim.anim.SetTrigger("Idle");
                            isIdle = true;
                        }
                        else if(x != 0 || z != 0)
                        {
                            isIdle = false;
                        }

                        if (isAttack && !r1)
                        {
                            if (!jump)
                            {
                                /*if (rage >= playerStatus.rageMax && Input.GetKeyDown(KeyCode.Joystick1Button5) && !crinos)
                                {
                                    playerStatus.pode = true;
                                    crinos = true;
                                    anim.GetComponent<SpriteRenderer>().color = Color.blue;
                                    anim.anim.runtimeAnimatorController = crinosAnim.GetComponent<Animator>().runtimeAnimatorController;
                                    StartCoroutine("Crinos");
                                }*/
                                if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Space))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    StopCombo();
                                    SocoFraco();                                    
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    SocoForte();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                                {
                                    Jump();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                                {
                                    block = true;
                                    anim.anim.SetBool("Block", true);
                                    isAttack = false;
                                    isRun = false;
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.Joystick1Button2) && jumpAttack == 0)
                                {
                                    anim.anim.SetTrigger("JumpAttack");
                                    jumpAttack++;
                                }
                            }
                            
                            if (Input.GetKey(KeyCode.Joystick1Button5))
                            {
                                r1 = true;
                            }
                        }

                        if (Input.GetKeyUp(KeyCode.Joystick1Button5))
                        {
                            r1 = false;
                        }

                        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
                        {
                            block = false;
                            anim.anim.SetBool("Block", false);
                            isAttack = true;
                            isRun = true;
                        }
                        break;

                    case PlayerContr.Player2:
                        if (isRun)
                        {
                            x = Input.GetAxis("HorizontalP2");
                            if (!jump)
                            {
                                z = Input.GetAxis("VerticalP2");
                            }
                            else
                            {
                                z = 0;
                            }
                            transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)));
                        }
                        else
                        {
                            x = 0;
                            z = 0;
                        }

                        if (Input.GetKey(KeyCode.Joystick2Button5))
                        {
                            r1 = true;
                        }
                        if (Input.GetKeyUp(KeyCode.Joystick2Button5))
                        {
                            r1 = false;
                        }

                        if (isAttack && !r1)
                        {
                            if (!jump)
                            {
                                if (Input.GetKeyDown(KeyCode.Joystick2Button2) || Input.GetKeyDown(KeyCode.Space))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    StopCombo();
                                    SocoFraco();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    SocoForte();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                                {
                                    Jump();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
                                {
                                    block = true;
                                    anim.anim.SetBool("Block", true);
                                    isAttack = false;
                                    isRun = false;
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                                {
                                    anim.anim.SetTrigger("JumpAttack");
                                }
                            }
                        }

                        if (Input.GetKeyUp(KeyCode.Joystick2Button1))
                        {
                            block = false;
                            anim.anim.SetBool("Block", false);
                            isAttack = true;
                            isRun = true;
                        }
                        break;
                }
            }
        }
    }

  /*  IEnumerator Crinos()
    {
        yield return new WaitForSeconds(30);
        anim.anim.runtimeAnimatorController = playerAnim.GetComponent<Animator>().runtimeAnimatorController;
        crinos = false;
        anim.GetComponent<SpriteRenderer>().color = Color.white;
        rage = 0;
    }*/

    IEnumerator Floodando()
    {
        yield return new WaitForSeconds(1.2f);
        flooda = 0;
    }

    IEnumerator GnoseStart()
    {
        yield return new WaitForSeconds(1);
        if (gnose < playerStatus.gnosiMax)
        {
            gnose += playerStatus.gnosiRegen;
        }
        GnoseRestart();
    }

    void GnoseRestart()
    {
        StopCoroutine("GnoseStart");
        StartCoroutine("GnoseStart");
    }

    void Jump()
    {
        if (isJump)
        {
            jump = true;
            anim.anim.SetBool("Jump", jump);
            rig.velocity = new Vector3(rig.velocity.x, 10, rig.velocity.z);
            jumpAttack = 0;
        }
    }

    void SocoForte()
    {
        if (!isGrab)
        {
            isAttack = false;
            isRun = false;
            anim.anim.SetTrigger("SocoForte");
            PlayCombo();
        }
        else
        {
            grabAnim.SetTrigger("GrabThrow");
            anim.anim.SetTrigger("GrabThrow");
        }
    }

    void SocoFraco()
    {   
        if (contador > 3)
        {
            contador = 0;
        }
        if (!isGrab)
        {
            contador++;
        }
        isAttack = false;
        isRun = false;
        switch (contador)
        {
            case 0:
                grabAnim.SetTrigger("GrabAttack");
                anim.anim.SetTrigger("GrabAttack");
                enemy.GetComponent<EnemyController>().Dano(playerStatus.dmg, false, gameObject);
                anim.audioInstance = FMODUnity.RuntimeManager.CreateInstance(anim.socoFraco);
                anim.audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                anim.audioInstance.start();
                break;
            case 1:
                anim.anim.SetTrigger("SocoFraco0");
                break;
            case 2:
                anim.anim.SetTrigger("SocoFraco1");
                break;
            case 3:
                anim.anim.SetTrigger("SocoFraco2");    
                break;
            default:
                isRun = true;
                isAttack = true;
                break;
        }
        PlayCombo();
    }

    public void PlayCombo()
    {
        StartCoroutine("GO");
    }

    public void StopCombo()
    {
        StopCoroutine("GO");
    }

    public void DanoMagico(float dmg, int tipo)
    {
        if (tipo == 1)
        {
            playerStatus.life -= (dmg * playerStatus.resistances);
        }
        else if(tipo == 2)
        {
            playerStatus.life -= (dmg * playerStatus.resistances2);
        }
    }

    IEnumerator ZeraSlam()
    {
        yield return new WaitForSeconds(4);
        dano = 0;
    }

    public void Dano(float dmg, GameObject inimigo)
    {
        enemy = inimigo;
        StopCoroutine("ZeraSlam");
        StartCoroutine("ZeraSlam");
        if (!presa)
        {
            if (!apanha)
            {
                if (dano > 3)
                {
                    if (gameObject.GetComponent<PlayerController>().enabled == true)
                    {
                        if ((inimigo.transform.position.x > transform.position.x) && transform.localScale.x < 0)
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                        else if ((inimigo.transform.position.x < transform.position.x) && transform.localScale.x > 0)
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                        if (playerStatus.life > 0)
                        {
                            if (block)
                            {
                                dmg = dmg * playerStatus.blockEffect;
                                audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                                audioInstanceCreator.start();
                                anim.anim.SetTrigger("DanoBlock");
                            }
                            else
                            {
                                if (!jump)
                                {
                                    stun = true;
                                    anim.anim.SetTrigger("Slam");
                                    jump = true;
                                    apanha = true;
                                }
                            }
                           // rage += playerStatus.rageRegen;
                            dmg -= playerStatus.dmgTrash;
                        }
                    }
                    dano = 0;
                }
                else
                {
                    if (gameObject.GetComponent<PlayerController>().enabled == true)
                    {
                        if ((inimigo.transform.position.x > transform.position.x && transform.localScale.x > 0) || (inimigo.transform.position.x < transform.position.x && transform.localScale.x < 0))
                        {
                            anim.anim.SetInteger("Frente", 0);
                        }
                        else if ((inimigo.transform.position.x < transform.position.x && transform.localScale.x > 0) || (inimigo.transform.position.x > transform.position.x && transform.localScale.x < 0))
                        {
                            anim.anim.SetInteger("Frente", 1);
                        }
                        if (playerStatus.life > 0)
                        {
                            if (block)
                            {
                                dmg = dmg * playerStatus.blockEffect;
                                audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                                audioInstanceCreator.start();
                                anim.anim.SetTrigger("DanoBlock");
                            }
                            else
                            {
                                if (!jump)
                                {
                                    stun = true;
                                    anim.anim.SetTrigger("Dano");
                                    apanha = true;
                                    dano++;
                                }
                            }
                            //rage += playerStatus.rageRegen;
                            dmg -= playerStatus.dmgTrash;
                        }
                    }
                    playerStatus.life -= dmg;
                }
            }
        }
        else
        {
            pegador.GetComponent<EnemyController>().anim.SetTrigger("Dano");
        }
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(0.75f);
        contador = 0;
        isRun = true;
        isAttack = true;
        StopCoroutine("GO");
    }

    public void Liberated(GameObject obj)
    {
        isRun = true;
        isAttack = true;
    }

    public void Ataca()
    {
        isAttack = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Chao")
        {
            anim.Liberated();
            jump = false;
            anim.anim.SetBool("Jump", jump);
        }
    }
}

public enum PlayerContr
{
    Player1,
    Player2,
    Player3,
    Player4
}