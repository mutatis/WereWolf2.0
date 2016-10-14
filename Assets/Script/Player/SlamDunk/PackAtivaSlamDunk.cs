using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PackAtivaSlamDunk : MonoBehaviour
{
    public Canvas canvas;

    public Slider sliPunhe;

    public GameObject sliAumenDimi;

    public Transform segue, pai;

    public int numPress;

    int contador, etapa;

    void Start()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        sliPunhe.maxValue = numPress;
    }

    void Update()
    {
        pai = Manager.manager.player[0].transform;
        segue.position = new Vector3(pai.position.x, pai.position.y + 3, pai.position.z);
        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (etapa == 0)
            {
                contador++;
            }
            sliPunhe.value = contador;
        }

        if(contador >= numPress && etapa == 0)
        {
            sliPunhe.gameObject.SetActive(false);
            sliAumenDimi.gameObject.SetActive(true);
            etapa = 1;
        }
    }
}