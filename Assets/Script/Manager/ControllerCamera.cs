using UnityEngine;
using System.Collections;

public class ControllerCamera : MonoBehaviour
{
    public FollowTarget gameCamera;

    bool pode;

    public void GG()
    {
        if (!pode)
        {
            gameCamera.cont++;
            gameCamera.segue = true;
            pode = true;
        }
    }
}
