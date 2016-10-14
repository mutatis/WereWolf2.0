using UnityEngine;
using System.Collections;

public class LuzCenario : MonoBehaviour
{
    public Vector2 minMax;

    public Animator anim;

    [FMODUnity.EventRef]
    public string luz;

    FMOD.Studio.EventInstance luzInstance;

    void Start()
    {
        luzInstance = FMODUnity.RuntimeManager.CreateInstance(luz);
        luzInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        luzInstance.start();
        StartCoroutine("Pisca");
    }

    IEnumerator Pisca()
    {
        yield return new WaitForSeconds(Random.Range(minMax.x, minMax.y));
        luzInstance = FMODUnity.RuntimeManager.CreateInstance(luz);
        luzInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        luzInstance.start();
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Pisca");
        Volta();
    }

    void Volta()
    {
        StopCoroutine("Pisca");
        StartCoroutine("Pisca");
    }
}
