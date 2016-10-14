using UnityEngine;
using System.Collections;

public class SelecaoFoto : MonoBehaviour
{
    public GameObject proximo;
    public GameObject anterior;

    public void Troca()
    {
        anterior.transform.SetAsLastSibling();
        proximo.transform.SetAsLastSibling();
    }
}
