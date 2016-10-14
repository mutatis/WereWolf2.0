using UnityEngine;
using System.Collections;

public class ManagerCocaina : MonoBehaviour
{
    public static ManagerCocaina cocaina;

    public GameObject[] pozinho;

    void Awake()
    {
        cocaina = this;
    }

    void Start()
    {
        pozinho = GameObject.FindGameObjectsWithTag("Pozinho");
    }
}
