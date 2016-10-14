using UnityEngine;
using System.Collections;

public class FBIAudio : MonoBehaviour
{
    private FMOD.Studio.ParameterInstance levelIntensity;
    private FMOD.Studio.EventInstance vol;

    [FMODUnity.EventRef]
    public string som;

    void Start()
    {
        Cursor.visible = false;
        vol = FMODUnity.RuntimeManager.CreateInstance(som);
        vol.start();
    }

    public void Up()
    {
        vol.getParameter("barulho", out levelIntensity);
        levelIntensity.setValue(10f);
    }

    public void Down()
    {
        vol.getParameter("barulho", out levelIntensity);
        levelIntensity.setValue(0f);
    }
}
