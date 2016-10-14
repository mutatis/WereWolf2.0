using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollAtributos : MonoBehaviour
{
    public RectTransform content;

    public float valor;

    public int tipo;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            content.position = new Vector3(content.position.x, valor, content.position.z);
        }
        if(tipo == 1 && valor > content.position.y)
        {
           
        }
    }
}
