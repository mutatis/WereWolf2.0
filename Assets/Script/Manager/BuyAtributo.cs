using UnityEngine;
using System.Collections;

public class BuyAtributo : MonoBehaviour
{
    public float[] value;

    public string nome = "Andarilho";

    [FMODUnity.EventRef]
    public string lapis;

    FMOD.Studio.EventInstance lapisRef;

    public int posNaLista;

    public PaiAtributosEdit meuNumero;

    int x;

    void Start()
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
    }

    void Update()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
        {
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    Upgrade();
                }
            
        }
        else if (SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
        {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick2Button0))
                {
                    Upgrade();
                }
        }
    }

    void Upgrade()
    {
        if (value[x] <= PlayerPrefs.GetFloat("XP"))
        {
            if (posNaLista == 0)
            {
                ManagerPlayerPontos.managerPontos.SetStrength(nome, 1);
            }
            else if (posNaLista == 1)
            {
                ManagerPlayerPontos.managerPontos.SetDexterity(nome, 1);
            }
            else if (posNaLista == 2)
            {
                ManagerPlayerPontos.managerPontos.SetStamina(nome, 1);
            }
            else if (posNaLista == 3)
            {
                ManagerPlayerPontos.managerPontos.SetCharisma(nome, 1);
            }
            else if (posNaLista == 4)
            {
                ManagerPlayerPontos.managerPontos.SetIntelligence(nome, 1);
            }
            else if (posNaLista == 5)
            {
                ManagerPlayerPontos.managerPontos.SetSpirit(nome, 1);
            }
            PlayerPrefs.SetFloat("XP", (value[x] * -1));
            x++;

            lapisRef = FMODUnity.RuntimeManager.CreateInstance(lapis);
            lapisRef.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            lapisRef.start();
        }
    }
}