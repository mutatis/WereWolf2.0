using UnityEngine;
using System.Collections;

public class InterfaceLifePlayer : MonoBehaviour
{
    public GameObject[] obj;

    void Start()
    {
        for(int i = 0; i < PlayerPrefs.GetInt("Players"); i++)
        {
            obj[i].SetActive(true);
        }
    }
}
