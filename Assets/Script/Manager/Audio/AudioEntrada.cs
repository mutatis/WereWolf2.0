using UnityEngine;
using System.Collections;

public class AudioEntrada : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string som;

    FMOD.Studio.EventInstance volEntrada;

    public string nomeCena;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        volEntrada = FMODUnity.RuntimeManager.CreateInstance(som);
        volEntrada.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
        volEntrada.start();
    }

    void Update()
    {
        if (Application.loadedLevelName != nomeCena)
        {
            volEntrada.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            volEntrada.release();
            Destroy(gameObject);
        }
        else
        {
            volEntrada.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
        }
    }
}