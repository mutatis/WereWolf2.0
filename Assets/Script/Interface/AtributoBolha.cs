using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtributoBolha : MonoBehaviour
{
    public Image[] img;

    public Sprite spt;

    public string nome = "Andarilho";

    public int posNaLista;

    public int x;

    void Update()
    {
        if (posNaLista == 0)
        {
            x = ManagerPlayerPontos.managerPontos.GetStrength(nome);
        }
        else if (posNaLista == 1)
        {
            x = ManagerPlayerPontos.managerPontos.GetDexterity(nome);
        }
        else if (posNaLista == 2)
        {
            x = ManagerPlayerPontos.managerPontos.GetStamina(nome);
        }
        else if (posNaLista == 3)
        {
            x = ManagerPlayerPontos.managerPontos.GetCharisma(nome);
        }
        else if (posNaLista == 4)
        {
            x = ManagerPlayerPontos.managerPontos.GetIntelligence(nome);
        }
        else if (posNaLista == 5)
        {
            x = ManagerPlayerPontos.managerPontos.GetSpirit(nome);
        }

        for (int i = 0; i < x; i++)
        {
            img[i].sprite = spt;
        }
    }
}
