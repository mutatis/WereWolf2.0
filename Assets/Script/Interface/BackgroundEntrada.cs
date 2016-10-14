using UnityEngine;
using System.Collections;

public class BackgroundEntrada : MonoBehaviour
{
    public Animator[] anim;

    public GameObject obj;

    public void Play()
    {
        obj.SetActive(true);
        for(int i = 0; i < anim.Length; i++)
        {
            anim[i].enabled = true;
        }
    }
}
