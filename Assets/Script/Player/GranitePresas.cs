using UnityEngine;
using System.Collections;

public class GranitePresas : MonoBehaviour
{
    public float vel, tempopara, tempomorre;

    bool para = true;

    [FMODUnity.EventRef]
    public string parede;

    FMOD.Studio.EventInstance volInicio;

    void Start()
    {
        StartCoroutine("GO");
        volInicio = FMODUnity.RuntimeManager.CreateInstance(parede);
        volInicio.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        volInicio.start();
    }

    void Update()
    {
        if(para)
        transform.Translate(0, vel, 0);
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(tempopara);
        para = false;
        yield return new WaitForSeconds(tempomorre);
        Sai();
    }

    public void Sai()
    {
        Destroy(gameObject);
    }
}