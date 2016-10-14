using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controllopcao : MonoBehaviour
{
    public Image[] img;
    public Slider sli;

    public void Aumenta()
    {
        if(sli.value < 1)
        {
            sli.value = sli.value + 0.1f;
        }
    }

    public void Diminui()
    {
        if (sli.value > 0)
        {
            sli.value = sli.value - 0.1f;
        }
    }

    public void Select()
    {
        for(int i = 0; i < img.Length; i++)
        {
            img[i].color = Color.green;
        }
    }

    public void Deselect()
    {
        for (int i = 0; i < img.Length; i++)
        {
            img[i].color = Color.white;
        }
    }
}
