using UnityEngine;
using System.Collections;

public class Mulher : MonoBehaviour
{
    public GameObject pacman, posInicial, objF;

    public Animator anim;

    public SpriteRenderer sprite;

    public Vector2 vel;

    public int tipo;

    GameObject target, encontro;

    Vector3 direction;

    Vector2 velInicial;

    bool roamming = true, volta, mudar;

    void Start()
    {
        velInicial = vel;
    }

	void Update ()
    {
        if(volta)
        {
            if(Vector3.Distance(transform.position, posInicial.transform.position) > 1)
            {
                objF.GetComponent<PacmanGame>().cu.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                objF.GetComponent<PacmanGame>().cu.release();
                direction = posInicial.transform.position - transform.position;
                direction.Normalize();
                transform.Translate(direction, Space.World);
            }
            else
            {
                volta = false;
            }
        }
        else
        {
            transform.Translate(vel, Space.World);
        }

        if(mudar)
        {
            if(Vector2.Distance(transform.position, encontro.transform.position) < 0.5f)
            {
                transform.position = encontro.gameObject.transform.position;
                vel = encontro.gameObject.GetComponent<DirecaoGhost>().vel[Random.Range(0, encontro.gameObject.GetComponent<DirecaoGhost>().vel.Length)];
                mudar = false;
            }
        }
	}

    public void Sexo(GameObject obj)
    {
        objF = obj;
        objF.GetComponent<PacmanGame>().cu.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        objF.GetComponent<PacmanGame>().cu.release();
        transform.position = posInicial.transform.position;
        vel = velInicial;
        target = posInicial;
        volta = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Roamming")
        {
            encontro = other.gameObject;
            mudar = true;
        }
    }
}
