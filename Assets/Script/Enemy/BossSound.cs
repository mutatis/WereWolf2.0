using UnityEngine;
using System.Collections;

public class BossSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string sdStart, sdJump;

    bool pode;

    FMOD.Studio.EventInstance volSoundBoss;

    public void SDStart()
    {
        volSoundBoss = FMODUnity.RuntimeManager.CreateInstance(sdStart);
        volSoundBoss.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        volSoundBoss.start();
        pode = true;
    }

    public void SDJump()
    {
        if (pode)
        {
            volSoundBoss = FMODUnity.RuntimeManager.CreateInstance(sdJump);
            volSoundBoss.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            volSoundBoss.start();
            pode = false;
        }
    }
}
