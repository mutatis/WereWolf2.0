using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public Animator animHead;

    public SpriteRenderer head;

    public Rigidbody rig;

    public ProbabilidadeEnemy probabilidade;

    public GameObject enemyAnim;

    public EnemyAnim enemyanim;

    public Vector3 deus;

    public bool stun;
    public bool dano = true;
    public bool roamming = true;
    public bool combate = true;
    public bool slam;
    public bool isEngage, procura, ativacao, combo, caindoSlam;

    public TextMesh text;
    
	public float tempoResposta, life, vel1, vel2, dist, distOportunidade;

    public int xp, tipo;

    public string[] attack;

    [HideInInspector]
    public GameObject peguei;
    public GameObject player, seta;

    public GameObject[] obj;

    bool isWalk = true;
    bool prepare = true;
    bool isIdle = true;
    bool isRun = true;
    bool taPego, chamei, block, costas, autorizo, denovo, caindo;

    Vector3 direction;
    
    float dist1, dist2;

    int danoEscolha, oportunidade;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine("Procura");
    }
    
    void Update()
    {
        if(caindoSlam)
        {
            if (transform.localScale.x > 0)
            {
                transform.Translate(0.1f, 0, 0, Space.World);
            }
            else
            {
                transform.Translate(-0.1f, 0, 0, Space.World);
            }
        }

        anim.SetBool("isWalk", isWalk);

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
        {
            slam = false;
        }

        if(taPego && player.GetComponent<PlayerEngage>().playerAttack.solta >= 10)
        {
            Solta();
        }

        if (peguei == null)
        {
            anim.gameObject.SetActive(true);
            head.enabled = false;
        }

        if (peguei ==  null) 
		{
			if (life <= 0) 
			{
                Morreu();
			}

            if (!stun)
            {
                if (Manager.manager.player[0].GetComponent<PlayerMovment>().transform.position.x > transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                else if (Manager.manager.player[0].GetComponent<PlayerMovment>().transform.position.x < transform.position.x && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }

            if ((roamming || player == null) && !stun) 
			{
                distOportunidade = Vector3.Distance(Manager.manager.player[0].transform.position, transform.position);

                if(distOportunidade > 4.5f)
                {
                    oportunidade = 0;
                }

				if (distOportunidade > 4.5f || oportunidade > 1)
                {
                    if (Time.timeScale != 0)
                    {
						transform.Translate(vel1, 0, vel2, Space.World);
                    }
                    if (vel1 == 0 && vel2 == 0)
                    {                        
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle") && caindo && !anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoEnemy")
                            && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
                            anim.SetTrigger("Idle");
                    }
                    else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRun") && !costas && !anim.GetCurrentAnimatorStateInfo(0).IsName("GangFaliing") && 
                        !anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoEnemy") && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
                    {
                        anim.SetTrigger("Run");
                    }
                    else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRunCostas") && costas && !anim.GetCurrentAnimatorStateInfo(0).IsName("GangFaliing") && 
                        !anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoEnemy") && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
                    {
                        anim.SetTrigger("Run");
                    }
                }
				else if(distOportunidade < 4.5f && !anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoEnemy")
                    && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
                {
                    anim.SetTrigger("SocoFraco0");
					oportunidade++;
                }

                if ((vel1 > 0 && transform.localScale.x > 0) || (vel1 < 0 && transform.localScale.x < 0))
                {
                    costas = true;
                    anim.SetBool("Costas", true);
                }
                else
                {
                    costas = false;
                    anim.SetBool("Costas", false);
                }
			}

			if (player != null) 
			{
				dist = Vector3.Distance (player.transform.position, transform.position);
			}

			if (dist > 1f && player != null && !isEngage)
            {
                isWalk = true;
                StartCoroutine ("Engage");
			} 
			else if (player != null && dist < 1 && !chamei) 
			{
				StartCoroutine ("Pode");
				chamei = true;
			} 
			else if (isWalk && player != null && dist > 2)
            {
                player.GetComponent<PlayerEngage>().enemy = null;
                player.GetComponent<PlayerEngage> ().engage--;
				player = null;
				StopCoroutine ("Pode");
				StartCoroutine ("Pode");
				isWalk = false;
			}
			else if (player == null) 
			{
				Prepare ();
			}

			if (player == null && procura) 
			{
				var x = Random.Range (0, Manager.manager.playerEngage.Length);
				if (Manager.manager.playerEngage [x].GetComponent<PlayerEngage> ().engage < 1) 
				{
					player = Manager.manager.playerEngage [x];
                    player.GetComponent<PlayerEngage>().enemy = gameObject;
                    player.GetComponent<PlayerEngage>().engage++;

                }
			}
		}	
        else
        {
            if(peguei.GetComponent<Agarra>().playerMov.transform.localScale.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (peguei.GetComponent<Agarra>().playerMov.transform.localScale.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            StopAllCoroutines();
            transform.position = new Vector3(peguei.transform.position.x, peguei.transform.position.y, peguei.transform.position.z + 0.5f);
        }

        if(!procura && !ativacao)
        {
            StartCoroutine(Procura());
        }
    }

    public void Morreu()
    {
        StopAllCoroutines();
        anim.SetBool("Morreu", true);
        anim.SetTrigger("Dead");
        if (player != null)
        {
            player.GetComponent<PlayerEngage>().enemy = null;
            player.GetComponent<PlayerEngage>().engage--;
            enemyanim.nome = player.GetComponent<PlayerEngage>().nome;
        }
        dano = false;
        gameObject.GetComponent<EnemyController>().enabled = false;

    }

    IEnumerator Procura()
    {
        ativacao = true;
        procura = false;
        var tempo = Random.Range(2f, 3f);
        yield return new WaitForSeconds(tempo);
        procura = true;
        ativacao = false;
    }

    public void Prepare()
    {
        if (prepare)
        {
            Wait();
            StopCoroutine("Foi");
            StartCoroutine("Foi");
            prepare = false;
        }
    }

    public void Para()
    {
        StopAllCoroutines();
    }

    IEnumerator Foi()
    {
        if (vel1 != 0 && caindo)
        {
            anim.SetTrigger("Run");
        }
        yield return new WaitForSeconds(4);
        prepare = true;
    }

    public void Combate()
    {
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        roamming = false;
        int num;
		if(!stun && dist < 0.75f && combate && !taPego && !combo)//&& !player.GetComponent<PlayerEngage>().playerAttack.playerAnim.levanta
        {
            num = probabilidade.ChooseAttack();	

            switch (num)
            {
                case 0:
                    Soco();
                    break;

                case 1:
                    StartCoroutine(Defesa());
                    break;

                case 2:
                    SpecialMove();
                    break;

                case 3:
                    Soco2();
                    break;

                case 4:
                    Grab();
                    break;

				case 6:
					StartCoroutine (Combo ());
					break;

                default:
                    Soco();
                    break;
            }
        }
        chamei = false;
    }

    IEnumerator Pode()
    {
        yield return new WaitForSeconds(tempoResposta);
        Combate();
    }

    void Grab()
    {
        if((player.GetComponent<PlayerEngage>().playerStats.transform.position.x > transform.position.x && 
            player.GetComponent<PlayerEngage>().playerStats.transform.localScale.x > 0) || 
            (player.GetComponent<PlayerEngage>().playerStats.transform.position.x < transform.position.x && 
            player.GetComponent<PlayerEngage>().playerStats.transform.localScale.x < 0) && player.GetComponent<PlayerEngage>().playerStats.playerStatus.life > 0)
        {
            if (!player.GetComponent<PlayerEngage>().playerMov.isGrab && !player.GetComponent<PlayerEngage>().playerStats.crinos && 
                !player.GetComponent<PlayerEngage>().playerStats.anim.levanta)
            {
                player.GetComponent<PlayerEngage>().playerAttack.playerAnim.gameObject.SetActive(false);
                player.GetComponent<PlayerEngage>().playerDano.stun = true;
                player.GetComponent<PlayerEngage>().playerDano.pegador = gameObject;
                player.GetComponent<PlayerEngage>().playerAttack.presa = true;
                anim.SetBool("isGrab", true);
                anim.SetTrigger("Grab");
                roamming = false;
                combate = false;
				taPego = true;
			}
        }
    }

    public void Solta()
    {
        anim.SetBool("isGrab", false);
        anim.SetTrigger("Idle");
        player.GetComponent<PlayerEngage>().playerAttack.playerAnim.gameObject.SetActive(true);
        player.GetComponent<PlayerEngage>().playerDano.stun = false;
        player.GetComponent<PlayerEngage>().playerAttack.solta = 0;
        player.GetComponent<PlayerEngage>().playerDano.pegador = null;
        player.GetComponent<PlayerEngage>().playerAttack.presa = false;
        anim.SetBool("isGrab", false);
        anim.SetTrigger("Idle");
        combate = true;
        taPego = false;
    }

    public void Switch()
    {
        //escolhe outro player
        StopCoroutine("Engage");
        isEngage = false;
        roamming = true;
        if (player != null)
        {
            if (player.GetComponent<PlayerEngage>().engage > 0)
            {
                player.GetComponent<PlayerEngage>().enemy = null;
                player.GetComponent<PlayerEngage>().engage--;
            }
        }
        player = null;
        procura = true;
    }

    public void Wait()
    {
        roamming = true;
        int y = Random.Range(1, 3);
        if (y == 1)
        {
            vel1 = 0.015f;
        }
        else
        {
            vel1 = -0.015f;
        }
        int x = Random.Range(1, 3);
        if (x == 1)
        {
            vel2 = 0.015f;
        }
        else
        {
            vel2 = -0.015f;
        }
    }

    void SpecialMove()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Defesa()
    {
        if (!autorizo)
        {
            roamming = false;
            combate = false;
            anim.SetTrigger("Block");
            block = true;
            yield return new WaitForSeconds(2f);
            combate = true;
            block = false;
            anim.SetTrigger("Idle");
            StopCoroutine("Autorizacao");
            StartCoroutine("Autorizacao");
        }
    }

    void Soco2()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
        {
            roamming = false;
            combate = false;
            anim.SetTrigger("SocoFraco1");
            StartCoroutine(Combo());
        }
    }

    void Soco()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
        {
            roamming = false;
            combate = false;
            anim.SetTrigger("SocoFraco0");
            StartCoroutine(Combo());
        }
    }

	IEnumerator Combo()
	{
		combo = true;
		yield return new WaitForSeconds (0.7f);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
            anim.SetTrigger("SocoFraco0");
		yield return new WaitForSeconds (0.3f);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
            anim.SetTrigger("SocoFraco0");
		yield return new WaitForSeconds (0.3f);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
            anim.SetTrigger("SocoFraco1");
		yield return new WaitForSeconds (2f);
		combo = false;
	}

    IEnumerator Engage()
    {
        isEngage = true;
        if (isIdle)
        {
            anim.SetTrigger("Idle");
            isIdle = false;
        }
        roamming = false;
        yield return new WaitForSeconds(0.5f);
        if (isRun)
        {
            anim.SetTrigger("Run");
            isRun = false;
        }
        if (player != null)
        {
            while (dist > 0.6f)
            {
                if (player != null)
                {
                    direction = player.transform.position - transform.position;
                    direction.Normalize();
                    if (!stun)
                    {
                        deus = (direction / 10);
                        if (Time.timeScale != 0 && !stun)
                        {
							transform.Translate(deus, Space.World);
                        }
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRun") && !costas)
                        {
                            anim.SetTrigger("Run");
                        }
                        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRunCostas") && costas)
                        {
                            anim.SetTrigger("Run");
                        }

                        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
                        {
                            anim.SetBool("Costas", false);
                            costas = false;
                        }
                        else
                        {
                            costas = true;
                            anim.SetBool("Costas", true);
                        }
                        combate = false;
                    }
                    dist = Vector3.Distance(player.transform.position, transform.position);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    var x = Random.Range(0, Manager.manager.playerEngage.Length);
                    if (Manager.manager.playerEngage[x].GetComponent<PlayerEngage>().engage < 1)
                    {
                        player = Manager.manager.playerEngage[x];
                        player.GetComponent<PlayerEngage>().enemy = gameObject;
                        player.GetComponent<PlayerEngage>().engage++;
                    }
                }
            }
            combate = true;
            isIdle = true;
            isRun = true;
            deus = new Vector3(0, 0, 0);
            isEngage = false;
            anim.SetTrigger("Idle");
            isWalk = false;
            StopCoroutine("Pode");
            StartCoroutine("Pode");
        }
        else
        {
            isEngage = false;
        }
    }


    public void DanoAgain()
    {
        combate = true;
        stun = false;
        roamming = false;
        dano = true;
        chamei = false;
        text.text = "";
    }

    public void Apanha()
    {
        dano = true;
    }

    IEnumerator Autorizacao()
    {
        yield return new WaitForSeconds(1);
        autorizo = false;
    }

    public void Dano(float dmg, bool crit, GameObject obj)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
        {
            if (taPego)
            {
                Solta();
            }
            roamming = false;
            anim.SetBool("isSlam", true);
            autorizo = true;
            StopCoroutine("Autorizacao");
            StartCoroutine("Autorizacao");
            if (peguei == null)
            {
                if (danoEscolha >= 2)
                {
                    danoEscolha = 0;
                }
                danoEscolha++;
            }
            else
            {
                anim.gameObject.SetActive(true);
                danoEscolha = 3;
            }
            anim.SetInteger("DanoEscolha", danoEscolha);
            if (dano)
            {
                if (!block)
                {
                    life -= dmg;
                    if (crit == true)
                    {
                        text.color = Color.red;
                        text.text = dmg.ToString() + " CRIT";
                    }
                    else
                    {
                        text.color = Color.black;
                        text.text = dmg.ToString();
                    }
                }
                else
                {
                    life -= (dmg / 2);
                    if (crit == true)
                    {
                        text.color = Color.red;
                        text.text = (dmg / 2).ToString() + " CRIT";
                    }
                    else
                    {
                        text.color = Color.white;
                        text.text = (dmg / 2).ToString();
                    }
                }
                stun = true;
                isWalk = false;
                chamei = true;
                StopCoroutine("Pode");
                if (!block)
                {
                    if (danoEscolha == 3)
                    {
                        animHead.SetTrigger("Dano");
                    }
                    anim.SetTrigger("Dano");
                }
                else
                {
                    anim.SetTrigger("BlockDmg");
                }
                dano = false;
            }
        }
    }

    public void Slam(float dmg, bool crit, GameObject obj, float knockback)
    {
        roamming = false;
        dano = false;
        anim.SetBool("isSlam", true);
        if (anim.gameObject.active)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
            {
                anim.SetTrigger("Slam");
            }
        }
        slam = true;
        life -= dmg;
        autorizo = true;
        StopCoroutine("Autorizacao");
        StartCoroutine("Autorizacao");
        if (crit == true)
        {
            text.color = Color.red;
            text.text = dmg.ToString() + " CRIT";
        }
        else
        {
            text.color = Color.white;
            text.text = dmg.ToString();
        }
        if (transform.localScale.x < 0)
        {
            rig.velocity = new Vector2((knockback * (-1)), knockback);
        }
        else if (transform.localScale.x > 0)
        {
            rig.velocity = new Vector2(knockback, knockback);
        }
        text.text = dmg.ToString();
        StopCoroutine("Pode");
        chamei = true;
        stun = true;
        caindoSlam = true;
        Switch();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = -0.015f;
            int x = Random.Range(1, 3);
            if(x == 1)
            {
                vel2 = 0.015f;
            }
            else
            {
                vel2 = -0.015f;
            }
        }
        if (other.gameObject.tag == "Parede2")
        {
            print("P2");
            vel1 = 0.015f;
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                vel2 = 0.015f;
            }
            else
            {
                vel2 = -0.015f;
            }
        }
        if(other.gameObject.tag == "Chao")
        {
            anim.SetTrigger("Run");
            caindo = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Chao")
        {
            caindo = true;
        }
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = -0.015f;
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                vel2 = 0.015f;
            }
            else
            {
                vel2 = -0.015f;
            }
        }
        if (other.gameObject.tag == "Parede2")
        {
            vel1 = 0.015f;
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                vel2 = 0.015f;
            }
            else
            {
                vel2 = -0.015f;
            }
        }
    }
}