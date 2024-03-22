using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera script responsible for following player character

public class CameraScript : MonoBehaviour
{
    
    public Transform player;
    Transform cameraTran;

    void Start()
    {
        cameraTran = Camera.main.transform;
    }

    void FixedUpdate()
    {
        Vector3 cameraPos = new Vector3(player.position.x, cameraTran.position.y, cameraTran.position.z - 1);
        Vector3 cameraSmoother = Vector3.Lerp(transform.position, cameraPos, 10f * Time.deltaTime);
        cameraTran.position = cameraSmoother;
    }
}
