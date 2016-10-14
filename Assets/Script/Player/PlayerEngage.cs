using UnityEngine;
using System.Collections;

public class PlayerEngage : MonoBehaviour
{
    public int engage;

    public GameObject enemy;

    public PlayerStats playerStats;

    public PlayerAttackController playerAttack;

    public PlayerDano playerDano;

    public PlayerMovment playerMov;

    public string nome;

    void Start()
    {
        nome = playerStats.nome;
    }

    void Update()
    {
        if(enemy == null)
        {
            engage = 0;
        }
        else
        {
            engage = 1;
        }
    }
}
