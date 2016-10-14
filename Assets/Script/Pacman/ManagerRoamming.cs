using UnityEngine;
using System.Collections;

public class ManagerRoamming : MonoBehaviour
{
    public static ManagerRoamming roamming;

    public GameObject[] posicao;
    
    void Awake()
    {
        roamming = this;
    }
    
	void Start ()
    {
        posicao = GameObject.FindGameObjectsWithTag("Roamming");
	}
}
