using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FXVolumeControl : MonoBehaviour
{
    public Slider sli;

    public int tipo;

    public Text text;

    void Start()
    {
        if (tipo == 0)
        {
            sli.value = PlayerPrefs.GetFloat("VolumeFX");
        }
        else if (tipo == 1)
        {
            sli.value = PlayerPrefs.GetFloat("VolumeMusica");
        }
    }

    void Update()
    {
        if (tipo == 0)
        {
            PlayerPrefs.SetFloat("VolumeFX", sli.value);
        }
        else if(tipo == 1)
        {
            PlayerPrefs.SetFloat("VolumeMusica", sli.value);
        }

        text.text = sli.value.ToString("F1");
    }
}
