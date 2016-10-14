using UnityEngine;
using System.Collections;

public class PutaFollowFantasma : MonoBehaviour
{
    public Transform fantasma;

	void Update ()
    {
        transform.position = new Vector3(fantasma.position.x - 0.6f, fantasma.position.y, fantasma.position.z - 0.2f);
	}
}
