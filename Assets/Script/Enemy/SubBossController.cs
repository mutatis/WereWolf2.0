using UnityEngine;
using System.Collections;

public class SubBossController : MonoBehaviour
{
    public Animator anim;

    public Rigidbody rig;

    public GameObject sprt;

    public ProbabilidadeEnemy probabilidade;

    public ProbabilidadeEnemy probabilidade2;

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

    public GameObject player, tiro;

    public int marcado;

    GameObject obj;

    int dir;

    bool isWalk = true;
    bool procura = true;
    bool prepare = true;
    bool sugando;

    Vector3 direction;

    void Start()
    {
        summon = Manager.manager.summoner;
        Wait();
        StartCoroutine("Pode");
        //Combate();
    }

    void Update()
    {
        if(player != null)
            dist = Vector3.Distance(player.transform.position, transform.position);

        if(sugando)
        {
            obj.GetComponent<PlayerStats>().rage -= 0.5f;
        }

        if (!perto)
        {
            if (marcado == 0)
            {
                transform.Translate(vel1, 0, vel2);
            }

            if (marcado == 1)
            {
                transform.Translate(new Vector3(0.001f, 0, -0.1f));
            }
            else if (marcado == 2)
            {
                transform.Translate(new Vector3(0.001f, 0, 0.1f));
            }
        }

        if (life <= 0)
        {
            StopAllCoroutines();
            anim.SetTrigger("Dead");
            dano = false;
            gameObject.GetComponent<SubBossController>().enabled = false;
        }

        if (player == null && procura)
        {
            var x = Random.Range(0, Manager.manager.posSubBoss.Length);
            dir = x;
            player = Manager.manager.posSubBoss[x];

        }
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
                switch(num)
                {
                    case 0:
                        StartCoroutine("Engage");
                        break;
                    case 1:
                        StartCoroutine(SugaFuria());
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
                        var esco = Random.Range(0, Manager.manager.posSubBoss.Length);
                        dir = esco;
                        player = Manager.manager.posSubBoss[esco];
                        StartCoroutine("Engage");
                        break;

                    case 3:
                        Wait();
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

    IEnumerator SugaFuria()
    {
        var x = Random.Range(0, Manager.manager.player.Length);
        obj = Manager.manager.player[x];
        StopCoroutine("Pode");
        sugando = true;
        yield return new WaitForSeconds(3);
        StartCoroutine("Pode");
        sugando = false;
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
        anim.SetTrigger("SocoForte");
    }

    void Soco()
    {
        roamming = false;
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Engage()
    {
        StopCoroutine("Pode");
        roamming = false;
        dano = false;
        while (dist > 0.5f)
        {
            dano = false;
            sprt.SetActive(false);
            direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate((direction * 10) * Time.deltaTime);
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
        sprt.SetActive(true);
        dano = true;
        Attack();
        Wait();
        marcado = 0;
        StopCoroutine("Engage");
    }

    void Attack()
    {
        GameObject obj = Instantiate(tiro);
        obj.GetComponent<TiroEnemy>().transform.position = transform.position;
        if (dir < 2)
        {
            obj.GetComponent<TiroEnemy>().obj = Manager.manager.posSubBoss[3];
        }
        else
        {
            obj.GetComponent<TiroEnemy>().obj = Manager.manager.posSubBoss[1];
        }
        StopCoroutine("Pode");
        StartCoroutine("Volta");
        roamming = true;
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
            if ((player.transform.localScale.x > 0 && transform.localScale.x < 0) || (player.transform.localScale.x < 0 && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            anim.SetTrigger("Dano");
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