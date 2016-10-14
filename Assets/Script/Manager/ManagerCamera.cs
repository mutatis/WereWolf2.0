using UnityEngine;
using System.Collections;

public class ManagerCamera : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string musica1;

    FMOD.Studio.EventInstance vol;

    public string nomeCena;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        vol = FMODUnity.RuntimeManager.CreateInstance(musica1);
        vol.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        vol.start();
    }

    void Update()
    {
        if (Application.loadedLevelName != nomeCena)
        {
            vol.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Destroy(gameObject);
        }
    }
}