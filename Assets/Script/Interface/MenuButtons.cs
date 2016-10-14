using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    public Animator anim;

    public void Desliga()
    {
        anim.enabled = false;
    }
}
