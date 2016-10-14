using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtributosNavigat : MonoBehaviour
{
    public PaiAtributosEdit meuNumero;

    public AtributoTroca troca;

    public GameObject[] img;

    public GameObject[] description;

    public RectTransform content;

    public float[] valor;

    public SelectPersonagem select;

    bool podeDpad = true;
    bool podeDpad2 = true;

    int x = 0;

    void Update()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero && troca.enabled == true)
        {
            if (podeDpad)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("DpadYP1") < 0)
                {
                    if (x < (img.Length - 1))
                        x++;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    if (valor[x] > content.anchoredPosition.y)
                    {
                        content.anchoredPosition = new Vector2(content.anchoredPosition.x, valor[x]);
                    }
                    podeDpad = false;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("DpadYP1") > 0)
                {
                    if (x > 0)
                        x--;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    if (valor[x] < content.anchoredPosition.y)
                    {
                        content.anchoredPosition = new Vector2(content.anchoredPosition.x, valor[x]);
                    }
                    podeDpad = false;
                }
            }
        }
        else if(SelectPersonagem.personagem.select2 == meuNumero.meuNumero && troca.enabled == true)
        {
            if (podeDpad2)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("DpadYP2") < 0)
                {
                    if (x < (img.Length - 1))
                        x++;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    if (valor[x] > content.anchoredPosition.y)
                    {
                        content.anchoredPosition = new Vector2(content.anchoredPosition.x, valor[x]);
                    }
                    podeDpad2 = false;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("DpadYP2") > 0)
                {
                    if (x > 0)
                        x--;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    if (valor[x] < content.anchoredPosition.y)
                    {
                        content.anchoredPosition = new Vector2(content.anchoredPosition.x, valor[x]);
                    }
                    podeDpad2 = false;
                }
            }
        }

        if (Input.GetAxisRaw("DpadYP1") == 0)
        {
            podeDpad = true;
        }
        if (Input.GetAxisRaw("DpadYP2") == 0)
        {
            podeDpad2 = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            x = 0;
        }
    }
}
