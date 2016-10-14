using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtributoTroca : MonoBehaviour
{
    public PaiAtributosEdit meuNumero;

    public Animator anim;

    public GameObject[] desliga;

    public GameObject[] liga;
    
    public Image euImg;

    public GameObject proximo;
    public GameObject anterior;

    bool podeDpad = true;
    bool podeDpad2 = true;

    int qual;

    void Update ()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
        {
            if (podeDpad)
            {
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP1") > 0 || Input.GetKeyDown(KeyCode.JoystickButton5))
                {
                    Proximo();
                }
				else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP1") < 0 || Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    Anterior();
                }
            }
        }
        else if (SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
        {
            if (podeDpad2)
            {
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP2") > 0 || Input.GetKeyDown(KeyCode.JoystickButton5))
                {
                    Proximo();
                }
				else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP2") < 0 || Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    Anterior();
                }
            }
        }

        if (Input.GetAxisRaw("DpadXP1") == 0)
        {
            podeDpad = true;
        }

        if (Input.GetAxisRaw("DpadXP2") == 0)
        {
            podeDpad2 = true;
        }
    }

    public void Frente()
    {
        euImg.color = Color.white;
        
        for(int i = 0; i < liga.Length; i++)
        {
            liga[i].SetActive(true);
        }
    }

    public void Meio()
    {
        euImg.color = new Color(0.6f, 0.6f, 0.6f, 1);

        for (int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }
    }

    public void Tras()
    {
        euImg.color = new Color(0.27f, 0.27f, 0.27f, 1);
        for (int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }
    }

    public void Passo()
    {
        if(qual == 1)
            {
                anterior.transform.SetAsLastSibling();  
                proximo.transform.SetAsLastSibling();
            proximo.GetComponent<Animator>().SetTrigger("Comeco");
            anterior.GetComponent<Animator>().SetTrigger("Comeco2");
        }
        else if(qual == 2)
        {
            proximo.transform.SetAsLastSibling();
            anterior.transform.SetAsLastSibling();
            anterior.GetComponent<Animator>().SetTrigger("Comeco");
            proximo.GetComponent<Animator>().SetTrigger("Comeco2");
        }
    }

    public void Fim()
    {
        if (qual == 1)
        {
            proximo.GetComponent<AtributoTroca>().enabled = true;
            gameObject.GetComponent<AtributoTroca>().enabled = false;
            anterior.GetComponent<AtributoTroca>().enabled = false;
        }
        else if (qual == 2)
        {
            anterior.GetComponent<AtributoTroca>().enabled = true;
            gameObject.GetComponent<AtributoTroca>().enabled = false;
            proximo.GetComponent<AtributoTroca>().enabled = false;
        }
    }

    void Proximo()
    {
        proximo.GetComponent<AtributoTroca>().enabled = false;
        anim.SetTrigger("Passo");
        qual = 1;
        podeDpad = false;
        podeDpad2 = false;
        proximo.SetActive(true);
    }

    void Anterior()
    {
        proximo.GetComponent<AtributoTroca>().enabled = false;
        anim.SetTrigger("Passo");
        qual = 2;
        podeDpad = false;
        podeDpad2 = false;
        anterior.SetActive(true);
    }
}
