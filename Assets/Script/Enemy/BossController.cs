using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Animator anim, t2, t3, t4;

    public Rigidbody rig;

    public GameObject sprt, salto, tiro, garra;

    public ProbabilidadeEnemy probabilidade;

	public ProbabilidadeEnemy probabilidade2; 

	[FMODUnity.EventRef]
	public string tiroSound;

	FMOD.Studio.EventInstance volBoss;

    [HideInInspector]
    public SpriteRowCreator summon;

    public EnemyAnim enemyanim;

    public bool stun;
    [HideInInspector]
    public bool dano = true;
    [HideInInspector]
    public bool roamming = false;
    public bool perto = false;

    public TextMesh text;

    public float tempoResposta;
    public float life;
    public float vel1, vel2, dist;

    public int xp;

    public string[] attack;

    public GameObject player;

    public int marcado, esco;

    GameObject obj;

    int contTiro = 0;

    int contSalto, contSaltoMax, quantTiro, estagio = 4;

    float lifeMax;

    bool isWalk = true;
    bool procura = true;
    bool prepare = true;
    public bool regen, isAttack;

    Vector3 direction;

    void Start()
    {
        lifeMax = life;
        summon = Manager.manager.summoner;
        Wait();
        StartCoroutine("Pode");
        StartCoroutine("Regen");
        //Combate();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if ((lifeMax / 4) * 3 > life && (lifeMax / 4) * 2 < life && estagio == 4)
            {
                estagio = 3;
                anim.runtimeAnimatorController = t2.GetComponent<Animator>().runtimeAnimatorController;
            }
            else if ((lifeMax / 4) * 2 > life && (lifeMax / 4) * 1 < life && estagio == 3)
            {
                estagio = 2;
                anim.runtimeAnimatorController = t3.GetComponent<Animator>().runtimeAnimatorController;
            }
            else if ((lifeMax / 4) * 1 > life && estagio == 2)
            {
                estagio = 1;
                anim.runtimeAnimatorController = t4.GetComponent<Animator>().runtimeAnimatorController;
            }

            if (player != null)
                dist = Vector3.Distance(player.transform.position, transform.position);

            if ((lifeMax / 4) * estagio > life && regen)
            {
                life += 0.05f;
            }

            if (life > (lifeMax * 0.5f))
            {
                contSaltoMax = 1;
            }
            else if (life > (lifeMax * 0.25f))
            {
                contSaltoMax = 3;
            }
            else
            {
                contSaltoMax = 5;
            }

            if ((vel1 > 0 && transform.localScale.x > 0) || (vel1 < 0 && transform.localScale.x < 0))
            {
                anim.SetBool("Costas", true);
            }
            else
            {
                anim.SetBool("Costas", false);
            }

            if (!stun && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossPrepareSalto") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawInicio") &&
                        !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawAcerto") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawAcertoLoop"))
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

            if (!perto && !isAttack)
            {
                if (marcado == 0)
                {
                    if (vel1 == 0 && vel2 == 0)
                    {
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossPrepareSalto") &&
                            !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawInicio") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawAcerto") &&
                            !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawAcertoLoop") &&
                            !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyDano") && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyDano2"))
                            anim.SetTrigger("Idle");
                    }
                    else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRun") && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRunCostas") && !perto &&
                        !anim.GetCurrentAnimatorStateInfo(0).IsName("BossPrepareSalto") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawInicio") &&
                        !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawAcerto") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossClawAcertoLoop") &&
                            !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyDano") && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyDano2"))
                    {
                        anim.SetTrigger("Run");
                    }
                    transform.Translate(vel1, 0, vel2, Space.World);
                }

                if (marcado == 1)
                {
                    transform.Translate(new Vector3(0.001f, 0, -0.1f), Space.World);
                }
                else if (marcado == 2)
                {
                    transform.Translate(new Vector3(0.001f, 0, 0.1f), Space.World);
                }
            }

            if (life <= 0)
            {
                StopAllCoroutines();
                anim.SetTrigger("Dead");
                dano = false;
                gameObject.GetComponent<BossController>().enabled = false;
            }

            if (player == null && procura)
            {
                var x = Random.Range(0, Manager.manager.posSubBoss.Length);
                player = Manager.manager.posSubBoss[x];

            }
        }
    }

    IEnumerator Regen()
    {
        regen = false;
        yield return new WaitForSeconds(2);
        regen = true;
    }

    IEnumerator Procura()
    {
        procura = false;
        yield return new WaitForSeconds(1);
        procura = true;
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

    IEnumerator Foi()
    {
        if (vel1 != 0)
        {
            anim.SetTrigger("Run");
        }
        if (vel1 > 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        else if (vel1 < 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        yield return new WaitForSeconds(1);
        prepare = true;
    }

    public void Combate()
    {
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        roamming = false;
        int num;
        if (!perto)
        {
            num = probabilidade2.ChooseAttack();
            roamming = false;
            switch (num)
            {
                case 0:
                    StartCoroutine("Engage");
                    break;
                case 1:
                    StartCoroutine("Tiro");
                    break;
                case 2:
                    Summoner();
                    break;
            }
        }
        else
        {
            num = probabilidade.ChooseAttack();
            switch (num)
            {
                case 0:
                    Soco();
                    break;

                case 1:
                    Defesa();
                    break;

                case 2:
                    StartCoroutine("Engage");
                    break;

                case 3:
					StartCoroutine("Tiro");
                    break;

                case 4:
                    Wait();
                    break;

                case 5:
                    Wait();
                    break;

                case 6:
                    Wait();
                    break;

                default:
                    Soco();
                    break;
            }
        }

    }

    IEnumerator Pode()
    {
        var tempo = tempoResposta;
        yield return new WaitForSeconds(tempo);
        Combate();
    }

    void Summoner()
    {
        summon.CreateSprites();
    }

    void Switch()
    {
        //escolhe outro player
        roamming = false;
        player = null;
        procura = true;
    }

    public void Wait()
    {
        roamming = true;
        vel1 = 0.05f * Random.Range(-2, 2);
        vel2 = 0.05f * Random.Range(-1, 2);
        //chama a animacao de idle e deixar ele parado perto do player
    }

    void SpecialMove()
    {
        roamming = false;
        anim.SetTrigger("SocoFraco2");
    }

    void Defesa()
    {
        roamming = false;
		anim.SetTrigger("SocoFraco1");
    }

    void Soco()
    {
        roamming = false;
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Engage()
    {
        esco = Random.Range(0, Manager.manager.posSubBoss.Length);
        player = Manager.manager.posSubBoss[esco];
        isAttack = true;
        contSalto = 0;
        StopCoroutine("Pode");
        roamming = false;
        dano = false;
        while (dist > 0.7f)
        {
            dano = false;
            direction = player.transform.position - transform.position;
            direction.Normalize();
			transform.Translate((direction * 10) * Time.deltaTime, Space.World);
            if (direction.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }

            dist = Vector3.Distance(player.transform.position, transform.position);
            yield return new WaitForEndOfFrame();
        }
        vel1 = 0;
        vel2 = 0;
        anim.SetTrigger("SaltoPrepare");
    }

    public void PodeSalta()
    {
        sprt.SetActive(true);
        dano = true;
        StartCoroutine("Attack");
        StopCoroutine("Engage");
    }

    IEnumerator Tiro()
    {
        isAttack = true;
        sprt.SetActive(true);
        StopCoroutine("Pode");
        roamming = false;

        if(contTiro < Manager.manager.tiroBoss1.Length)
            player = Manager.manager.tiroBoss1[contTiro];

        dist = Vector3.Distance(player.transform.position, transform.position);

        if (contTiro < Manager.manager.tiroBoss1.Length)
        {
            while (dist > 0.6f)
            {
                direction = player.transform.position - transform.position;
                direction.Normalize();
				transform.Translate((direction * 20) * Time.deltaTime, Space.World);
                if (direction.x > 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0 && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }

                dist = Vector3.Distance(player.transform.position, transform.position);
                yield return new WaitForEndOfFrame();
            }
			volBoss = FMODUnity.RuntimeManager.CreateInstance(tiroSound);
			volBoss.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
			volBoss.start();
            GameObject obj = Instantiate(tiro);
            obj.GetComponent<TiroEnemy>().transform.position = transform.position;
            obj.GetComponent<TiroEnemy>().obj = gameObject;
            contTiro += 1;
            StartCoroutine("Tiro");
        }
        else
        {
            vel1 = 0;
            vel2 = 0;
            salto.SetActive(false);
            yield return new WaitForSeconds(2);
            contTiro = 0;
            vel1 = 0.05f * Random.Range(-2, 2);
            vel2 = 0.05f * Random.Range(-1, 2);
            marcado = 0;
            StartCoroutine("Volta");
            StopCoroutine("Tiro");
        }

    }

    IEnumerator Attack()
    {
        isAttack = true;
        salto.SetActive(true);
        StopCoroutine("Pode");
        roamming = false;
        if (esco < 2)
        {
            esco = Random.Range(2, 4);
        }
        else
        {
            esco = Random.Range(0, 2);
        }
        if (player != Manager.manager.posSubBoss[esco])
        {
            player = Manager.manager.posSubBoss[esco];
        }
        else
        {
            esco = Random.Range(0, Manager.manager.posSubBoss.Length);
            player = Manager.manager.posSubBoss[esco];
        }
        dist = Vector3.Distance(player.transform.position, transform.position);
        if (contSalto <= contSaltoMax)
        {
            anim.SetTrigger("ClawNormal");
            while (dist > 0.4f)
            {
                direction = player.transform.position - transform.position;
                direction.Normalize();
				transform.Translate((direction * 20) * Time.deltaTime, Space.World);
                if (direction.x > 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0 && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }

                dist = Vector3.Distance(player.transform.position, transform.position);
                yield return new WaitForEndOfFrame();
            }
            contSalto += 1;
            garra.SetActive(false);
            anim.SetTrigger("Idle");
            vel1 = 0;
            vel2 = 0;
            yield return new WaitForSeconds(1);
            StartCoroutine("Attack");
        }
        else
        {
            isAttack = false;
            anim.SetTrigger("Idle");
            vel1 = 0;
            vel2 = 0;
            salto.SetActive(false);
            yield return new WaitForSeconds(2);
            vel1 = 0.05f * Random.Range(-2, 2);
            vel2 = 0.05f * Random.Range(-1, 2);
            marcado = 0;
            StartCoroutine("Volta");
            StopCoroutine("Attack");
        }

    }

    IEnumerator Volta()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine("Pode");
        StopCoroutine("Volta");
    }

    IEnumerator GO()
    {
        roamming = false;
        yield return new WaitForSeconds(5);
        isWalk = true;
    }

    public void DanoAgain()
    {
        roamming = false;
        dano = true;
        text.text = "";
    }

    public void Apanha()
    {
        dano = true;
    }

    public void Dano(float dmg, bool crit, GameObject obj)
    {
        roamming = false;
        if (dano)
        {
            life -= dmg;
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
            if (player == null)
            {
                player = obj;
            }
            stun = true;
            isWalk = false;
            StopCoroutine("GO");
            StartCoroutine("GO");
            StopCoroutine("Regen");
            StartCoroutine("Regen");
            if ((player.transform.localScale.x > 0 && transform.localScale.x < 0) || (player.transform.localScale.x < 0 && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            anim.SetTrigger("Dano");
            anim.SetInteger("DanoEscolha", 1);
            dano = false;
        }
    }

    public void Slam(float dmg, bool crit, GameObject obj, float knockback)
    {
        roamming = false;
        dano = false;
        life -= dmg;
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
        if (player == null)
        {
            player = obj;
        }
        StopCoroutine("Regen");
        StartCoroutine("Regen");
        text.text = dmg.ToString();
        stun = true;
        anim.SetTrigger("Slam");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = 0.05f * Random.Range(-2, -0.2f);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
        if (other.gameObject.tag == "Parede2")
        {
            vel1 = 0.05f * Random.Range(0.2f, 2);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = 0.05f * Random.Range(-2, -0.2f);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
        if (other.gameObject.tag == "Parede2")
        {
            vel1 = 0.05f * Random.Range(0.2f, 2);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
    }
}