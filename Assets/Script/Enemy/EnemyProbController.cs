using UnityEngine;
using System.Collections;

public class EnemyProbController : MonoBehaviour
{
    public EnemyController enemyController;

    public ProbabilidadeEnemy probEnemy;

    public float tempProbabilidade, tempGrab;

	void Update ()
    {
        if (transform.position.x > Manager.manager.player[0].transform.position.x && Manager.manager.player[0].GetComponent<PlayerMovment>().transform.localScale.x < 0 ||
            transform.position.x < Manager.manager.player[0].transform.position.x && Manager.manager.player[0].GetComponent<PlayerMovment>().transform.localScale.x > 0 ||
            Manager.manager.player[0].GetComponent<PlayerAttackController>().presa)
        {
            foreach (Attack elem in probEnemy.attack)
            {
                if (elem.attack == "Defesa")
                {
                    elem.probalidade = 0;
                }
            }
        }
        else
        {
            foreach (Attack elem in probEnemy.attack)
            {
                if (elem.attack == "Defesa")
                {
                    elem.probalidade = tempProbabilidade;
                }
            }
        }

        if(Manager.manager.enemy.Length < 2)
        {
            foreach (Attack elem in probEnemy.attack)
            {
                if (elem.attack == "Grab")
                {
                    elem.probalidade = 0;
                }
            }
        }
        else
        {
            foreach (Attack elem in probEnemy.attack)
            {
                if (elem.attack == "Grab")
                {
                    elem.probalidade = tempGrab;
                }
            }
        }        
	}
}