using UnityEngine;
using System.Collections;

public class ProbabilidadeEnemy : MonoBehaviour
{ 
    //attack
    public Attack[] attack;

    //escolhe attack
    public int ChooseAttack()
    {
        float total = 0;
        int i = 0;
        foreach (Attack elem in attack)
        {
            total += elem.probalidade;
        }

        float randomPoint = Random.value * total;

        for (i = 0; i < attack.Length; i++)
        {
            if (randomPoint < attack[i].probalidade)
                return i;
            else
                randomPoint -= attack[i].probalidade;
        }

        return attack.Length - 1;
    }
}

[System.Serializable]
public class Attack
{
    public string attack;
    [Range(0, 100)]
    public float probalidade;
}
