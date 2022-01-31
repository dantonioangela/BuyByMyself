using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    new private Camera camera;
    public Player_Controller player_controller;
    private int oldMask;

    private void Start()
    {
        oldMask = Camera.main.cullingMask;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if( player_controller.inventario)
        {
            camera.cullingMask = (oldMask << LayerMask.NameToLayer("UI_inventario"));
        }
        else
        {
            camera.cullingMask = oldMask;
        }
    }
}
