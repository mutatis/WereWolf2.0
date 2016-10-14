using UnityEngine;
using System.Collections;

public class AtributaSetinha : MonoBehaviour
{
    public PaiAtributosEdit meuNumero;

    public Animator anim;

    public int tipo;

    [FMODUnity.EventRef]
    public string socoFraco;

    FMOD.Studio.EventInstance heal;

    bool podeDpad = true;
    bool podeDpad2 = true;

    void Update ()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
        {
            if (podeDpad)
            {
                if (tipo == 0)
                {
					if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP1") > 0 || Input.GetKeyDown(KeyCode.JoystickButton5))
                    {
                        anim.SetTrigger("Aperto");
                        heal = FMODUnity.RuntimeManager.CreateInstance(socoFraco);
                        heal.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                        heal.start();
                    }
                }
                else if (tipo == 1)
                {
					if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP1") < 0 || Input.GetKeyDown(KeyCode.JoystickButton4))
                    {
                        anim.SetTrigger("Aperto");
                        heal = FMODUnity.RuntimeManager.CreateInstance(socoFraco);
                        heal.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                        heal.start();
                    }
                }
                podeDpad = false;
            }
        }
        else if (SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
        {
            if (podeDpad2)
            {
                if (tipo == 0)
                {
					if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP2") > 0 || Input.GetKeyDown(KeyCode.JoystickButton5))
                    {
                        anim.SetTrigger("Aperto");
                    }
                }
                else if (tipo == 1)
                {
					if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP2") < 0 || Input.GetKeyDown(KeyCode.JoystickButton4))
                    {
                        anim.SetTrigger("Aperto");
                    }
                }
                podeDpad2 = false;
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
}