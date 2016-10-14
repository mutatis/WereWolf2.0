using UnityEngine;
using System.Collections;

public class NarigoAnimControll : MonoBehaviour
{
    public PacmanGame pac;

    public void Comeu()
    {
        pac.Gozo();
    }

    public void Precoce()
    {
        pac.Precoce();
    }
}
