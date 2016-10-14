using UnityEngine;
using System.Collections;

public class TimeCreator : MonoBehaviour
{
    public ControllerCamera camera;

    public SpriteRowCreator[] creator;

    public int[] quant;

    public float[] tempo;

    int num;

    public int x, controllerTempo;

	void Start ()
    {
        num = quant[0];
        FollowTarget.follow.quant = num;
        StartCoroutine("GO");
	}

    IEnumerator GO()
    {
        yield return new WaitForSeconds(tempo[controllerTempo]);
        if (x > num)
        {
            camera.GG();
            for (int i = 0; i < creator.Length; i++)
            {
                creator[i].enabled = false;
                StopCoroutine("GO");
            }
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                x++;
                if (x <= num)
                {
                    creator[Random.Range(0, creator.Length)].CreateSprites();
                }
            }
        }
        Denovo();
    }

    void Denovo()
    {
        controllerTempo++;
        num = quant[controllerTempo];
        x = 0;
        StopCoroutine("GO");
        StartCoroutine("GO");
    }
}