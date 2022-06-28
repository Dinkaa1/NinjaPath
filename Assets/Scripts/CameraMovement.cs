using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.position + offset;
    }
}
