using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerAttackController playerAttack;
    public PlayerDano playerDano;

    public Rigidbody rig;

    public Animator gotinha;

    public SpriteRenderer sprite;

    FMOD.Studio.EventInstance audioInstanceCreator;

    [FMODUnity.EventRef]
    public string jumpSound, jumpEnd;

    public bool run, toca, toca2;
    [HideInInspector]
    public bool isMov, jump, isGrab, isJump, acerto, completo;

    public float x, z, xButton, zButton;

    public int contInput, xRun = 1, tipo, pulo;

    float forcaY, dist, distTemp;

    Vector3 dir;

    void Start()
    {
        JumpStart(7, 19);
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, Manager.manager.player[0].transform.position);
        if(playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("StartSlamDunkLILI") && dist > 0.2f && !acerto)
        {
            distTemp = dist;
        }
        else if((playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("StartSlamDunkLILI") || playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SlamLiliPulo") ||
            playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SlamLiliPulo2") || playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SlamLiliPulo3")) &&
            dist > 0.2f && acerto)
        {
            if (dist <= distTemp/1.8f)
            {
                playerAnim.anim.SetTrigger("PuloSlam2");
            }
            dir = Manager.manager.player[0].transform.position - transform.position;
            dir.Normalize();
            transform.Translate(dir/7, Space.World);
        }
        else if ((playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("StartSlamDunkLILI") || playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SlamLiliPulo") ||
            playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SlamLiliPulo2") || playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SlamLiliPulo3") || 
            playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("ErroSlamLili")))
        {
            if (completo)
            {
                Manager.manager.player[0].GetComponent<PlayerStats>().playerAnim.SetTrigger("DesceSlam");
                playerAnim.anim.SetTrigger("AcertoSlam");
            }
            else
            {
                if(!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("ErroSlamLili"))
                {
                    gotinha.SetTrigger("Erro");
                    playerAnim.anim.SetTrigger("ErroSlam");
                }
                if(dist < 1.5f)
                {
                    transform.Translate(0.03f, 0, 0, Space.World);
                }
                else if (dist < 5)
                {
                    transform.Translate(0.1f, 0, 0, Space.World);
                }
                else
                {
                    acerto = false;
                    for (int i = 0; i < Manager.manager.player.Length; i++)
                    {
                        Manager.manager.player[i].GetComponent<PlayerStats>().playerAnim.SetBool("SlamDunk", false);
                    }
                    completo = false;
                }
            }
        }

		if (Time.timeScale != 0)
		{
            if (!playerAttack.mov)
            {
                transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)), Space.World);
            }

            if (tipo == 0)
            {
                if (playerStats.crinos && !sprite.flipX)
                {
                    sprite.flipX = true;
                }
                else if (!playerStats.crinos && sprite.flipX)
                {
                    sprite.flipX = false;
                }
            }

            switch (playerStats.player)
            {
                case Player.Player1:
                    if (!isMov)
                    {
                        playerAnim.anim.SetInteger("ContInput", xRun);
                        if (!playerAttack.jumpAttack)
                        {
                            if (Input.GetAxisRaw("DpadXP1") > 0 || Input.GetKey(KeyCode.RightArrow))
                            {
                                xButton = 0.13f;
                            }
                            else if (Input.GetAxisRaw("DpadXP1") < 0 || Input.GetKey(KeyCode.LeftArrow))
                            {
                                xButton = -0.13f;
                            }
                            else
                            {
                                xButton = 0;
                            }
                            x = (Input.GetAxis("HorizontalP1") + xButton) * xRun;
                            if ((x > 0 || x < 0) && !run && contInput == 0 && playerStats.crinos)
                            {
                                StartCoroutine("Run");
                            }
                        }
                        if (!jump && !playerAttack.block && !playerDano.stun)
                        {
                            if (Input.GetAxisRaw("DpadYP1") > 0 || Input.GetKey(KeyCode.UpArrow))
                            {
                                zButton = 0.38f;
                            }
                            else if (Input.GetAxisRaw("DpadYP1") < 0 || Input.GetKey(KeyCode.DownArrow))
                            {
                                zButton = -0.38f;
                            }
                            else
                            {
                                zButton = 0;
                            }
                            z = (Input.GetAxis("VerticalP1") + zButton) * 2 * xRun;
                            if (((Input.GetKeyDown(KeyCode.Joystick1Button0) && !Input.GetKey(KeyCode.Joystick1Button5)) ||
                                Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)) && !isJump && Time.timeScale != 0 && !playerStats.crinos)
                            {
                                Jump(10);
                            }
                            playerAnim.anim.SetFloat("RigVel", 0);
                        }
                        else if (jump && !playerAttack.block && !playerDano.stun)
                        {
                            z = 0;
                            playerAnim.anim.SetFloat("RigVel", rig.velocity.y);
                        }
                        else if (playerAttack.block || playerDano.stun)
                        {
                            x = 0;
                            z = 0;
                        }
                    }
                    else
                    {
                        x = 0;
                        z = 0;
                    }
                    break;

                case Player.Player2:
                    if (!isMov)
                    {
                        playerAnim.anim.SetInteger("ContInput", xRun);
                        if (!playerAttack.jumpAttack)
                        {
                            if (Input.GetAxisRaw("DpadXP2") > 0 || Input.GetKey(KeyCode.RightArrow))
                            {
                                xButton = 0.13f;
                            }
                            else if (Input.GetAxisRaw("DpadXP2") < 0 || Input.GetKey(KeyCode.LeftArrow))
                            {
                                xButton = -0.13f;
                            }
                            else
                            {
                                xButton = 0;
                            }
                            x = (Input.GetAxis("HorizontalP2") + xButton) * xRun;
                            if ((x > 0 || x < 0) && !run && contInput == 0 && playerStats.crinos)
                            {
                                StartCoroutine("Run");
                            }
                        }
                        if (!jump && !playerAttack.block && !playerDano.stun)
                        {
                            if (Input.GetAxisRaw("DpadYP2") > 0 || Input.GetKey(KeyCode.UpArrow))
                            {
                                zButton = 0.38f;
                            }
                            else if (Input.GetAxisRaw("DpadYP2") < 0 || Input.GetKey(KeyCode.DownArrow))
                            {
                                zButton = -0.38f;
                            }
                            else
                            {
                                zButton = 0;
                            }
                            z = (Input.GetAxis("VerticalP2") + zButton) * 2 * xRun;
                            if (((Input.GetKeyDown(KeyCode.Joystick2Button0) && !Input.GetKey(KeyCode.Joystick2Button5)) ||
                                Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)) && !isJump && Time.timeScale != 0 && !playerStats.crinos)
                            {
                                Jump(10);
                            }
                            playerAnim.anim.SetFloat("RigVel", 0);
                        }
                        else if (jump && !playerAttack.block && !playerDano.stun)
                        {
                            z = 0;
                            playerAnim.anim.SetFloat("RigVel", rig.velocity.y);
                        }
                        else if (playerAttack.block || playerDano.stun)
                        {
                            x = 0;
                            z = 0;
                        }
                    }
                    else
                    {
                        x = 0;
                        z = 0;
                    }
                    break;
            }

			

			if (playerAnim.anim.GetCurrentAnimatorStateInfo (0).IsName ("GrabWalkAndarilho")) 
			{
				if (playerAttack.enemy != null) {
					playerAttack.enemy.GetComponent<EnemyController> ().anim.gameObject.SetActive (true);
					playerAttack.enemy.GetComponent<EnemyController> ().head.enabled = true;
					if (!playerAttack.enemy.GetComponent<EnemyController> ().anim.GetCurrentAnimatorStateInfo (0).IsName ("EnemyInGrabRun")) {
						playerAttack.enemy.GetComponent<EnemyController> ().anim.SetBool ("Preso", true);
						playerAttack.enemy.GetComponent<EnemyController> ().anim.SetTrigger ("Run");
						playerAttack.enemy.GetComponent<EnemyController> ().animHead.SetTrigger ("Run");
					}
				}
			}

			if (((x != 0 || z != 0) && ((!playerAnim.anim.GetCurrentAnimatorStateInfo (0).IsName ("RunAndarilho") && !isGrab) ||
			        (!playerAnim.anim.GetCurrentAnimatorStateInfo (0).IsName ("GrabWalkAndarilho") && isGrab))) && ((!toca || toca && !toca2 && z != 0) && (!toca2 || !toca && toca2 && x != 0))) 
			{
				playerAnim.anim.SetTrigger ("Run");
			} 
			else if ((x == 0 && z == 0 && ((!playerAnim.anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleAndarilho") && !isGrab) ||
			             (!playerAnim.anim.GetCurrentAnimatorStateInfo (0).IsName ("GrabIdleAndarilho") && isGrab) ||
							(!playerAnim.anim.GetCurrentAnimatorStateInfo (0).IsName ("GrabIdleFomor") && isGrab))) || (toca && z == 0 || toca2 && x == 0)) 
			{
				playerAnim.anim.SetTrigger ("Idle");
			}

			if (x > 0 && transform.localScale.x < 0) 
			{
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			} 
			else if (x < 0 && transform.localScale.x > 0) 
			{
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}

			if (run && x == 0) 
			{
				contInput = 1;
			}

			if (run && (x > 0 || x < 0) && contInput == 1) 
			{
				Corre ();
			}

			if (xRun > 1 && x == 0) 
			{
				xRun = 1;
				run = false;
				contInput = 0;
			}
		}
    }

    void Corre()
    {
        run = false;
        contInput = 0;
        xRun = 2;
    }

    IEnumerator Run()
    {
        run = true;
        yield return new WaitForSeconds(0.5f);
        contInput = 0;
        run = false;
    }

    void JumpStart(float forceX, float forceY)
    {
        audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(jumpSound);
        audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstanceCreator.start();
        jump = true;
        playerAnim.anim.SetTrigger("Pulo");
        playerAnim.anim.SetBool("Jump", jump);
        rig.velocity = new Vector3(forceX, forceY, rig.velocity.z);
    }

    public void JumpUp()
    {
        if (pulo > 0)
        {
            jump = true;
            rig.velocity = new Vector3(rig.velocity.x, forcaY, rig.velocity.z);
        }
    }

    void Jump(float force)
    {
        pulo++;
        audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(jumpSound);
        audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstanceCreator.start();
        playerAnim.anim.SetTrigger("Pulo");
        playerAnim.anim.SetBool("Jump", true);
        forcaY = force;
        if (tipo != 1)
        {
            jump = true;
            rig.velocity = new Vector3(rig.velocity.x, force, rig.velocity.z);
        }
    }

    void Normal()
	{
		audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(jumpEnd);
		audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
		audioInstanceCreator.start();
        playerAnim.anim.SetTrigger("Falling");
    }

    public void CaboPulo()
    {
        isMov = false;
        playerAttack.jumpAttack = false;
        jump = false;
        playerAnim.anim.SetBool("Jump", jump);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Chao" && jump)
        {
            Normal();
            isMov = true;
            Manager.manager.parede.SetActive(true);
        }

        if(other.gameObject.tag == "Parede")
        {
            toca = true;
        }

        if(other.gameObject.tag == "Parede4")
        {
            toca2 = true;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Parede")
        {
            toca = true;
        }

        if (other.gameObject.tag == "Parede4")
        {
            toca2 = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Parede")
        {
            toca = false;
        }

        if (other.gameObject.tag == "Parede4")
        {
            toca2 = false;
        }
    }
}