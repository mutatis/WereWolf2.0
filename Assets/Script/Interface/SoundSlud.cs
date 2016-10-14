using UnityEngine;
using System.Collections;

public class SoundSlud : MonoBehaviour
{
    FMOD.Studio.EventInstance sludgeSound;

    [FMODUnity.EventRef]
    public string sludg;

    void Start ()
    {
        sludgeSound = FMODUnity.RuntimeManager.CreateInstance(sludg);
        sludgeSound.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        sludgeSound.start();
    }
}
