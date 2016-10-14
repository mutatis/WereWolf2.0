using UnityEngine;
using System.Collections;

public class MusicaManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string musica1;

    private FMOD.Studio.ParameterInstance levelIntensity;

    [FMODUnity.EventRef]
    public string musica2;

    [FMODUnity.EventRef]
    public string musica3;

    FMOD.Studio.EventInstance vol;
    FMOD.Studio.EventInstance vol2;
    FMOD.Studio.EventInstance vol3;

    public string nomeCena;

    float temp;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {        
        vol = FMODUnity.RuntimeManager.CreateInstance(musica1);
        vol.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        vol.start();

        vol2 = FMODUnity.RuntimeManager.CreateInstance(musica2);
        vol2.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        vol2.start();

        vol3 = FMODUnity.RuntimeManager.CreateInstance(musica3);
        vol3.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        vol3.start();
    }

    void Update()
    {
        if (Application.loadedLevelName != nomeCena)
        {
            vol.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            vol.release();
            vol3.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            vol.release();

        }
        vol.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
        vol2.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
        vol3.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
    }

    public void Mudo()
    {
        vol2.getParameter("transition_final", out levelIntensity);
        levelIntensity.setValue(80f);
    }
}
