using UnityEngine;
using System.Collections;

public class SetGifts : MonoBehaviour
{
    public SelectPersonagem atributo;

    public string nome = "Andarilho";

    public int skill;

    [FMODUnity.EventRef]
    public string lapis;

    FMOD.Studio.EventInstance lapisRef;

    public float value;

    public GameObject select;

    public PaiAtributosEdit meuNumero;

    void Update()
    {
        if (!select.activeSelf)
        {
            if (PlayerPrefs.GetInt(nome) == 1)
            {
                if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
                {
                    if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
                    {
                        atributo.enabled = false;
                        select.SetActive(true);
                    }
                }
                else if (SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
                {
                    if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick2Button0)))
                    {
                        atributo.enabled = false;
                        select.SetActive(true);
                    }
                }
            }
            else if (PlayerPrefs.GetInt(nome) == 0)
            {
                if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
                {
                    if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && value <= PlayerPrefs.GetFloat("XP"))
                    {
                        PlayerPrefs.SetInt(nome, 1);
                        PlayerPrefs.SetFloat("XP", (value * -1));
                        lapisRef = FMODUnity.RuntimeManager.CreateInstance(lapis);
                        lapisRef.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                        lapisRef.start();
                    }
                }
                else if ((SelectPersonagem.personagem.select2 == meuNumero.meuNumero) && value <= PlayerPrefs.GetFloat("XP"))
                {
                    if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick2Button0)) && value <= PlayerPrefs.GetFloat("XP"))
                    {
                        PlayerPrefs.SetInt(nome, 1);
                        PlayerPrefs.SetFloat("XP", (value * -1));
                        lapisRef = FMODUnity.RuntimeManager.CreateInstance(lapis);
                        lapisRef.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                        lapisRef.start();
                    }
                }
            }
        }        
    }
}