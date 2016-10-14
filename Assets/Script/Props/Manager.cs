using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    public static Manager manager;

    public GameObject[] player;
    public GameObject[] playerEngage;
    public GameObject[] enemy;
    public GameObject[] subBoss;
    public GameObject[] boss;
    public GameObject[] posSubBoss;
    public GameObject[] tiroBoss1;
    public GameObject parede;

    public SpriteRowCreator summoner;

    void Awake()
    {
        manager = this;
    }

    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        playerEngage = GameObject.FindGameObjectsWithTag("PlayerEngage");

        enemy = GameObject.FindGameObjectsWithTag("Enemy");

        subBoss = GameObject.FindGameObjectsWithTag("SubBoss");

        boss = GameObject.FindGameObjectsWithTag("Boss");
    }
}