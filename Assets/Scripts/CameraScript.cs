using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Rigidbody cam;
    public Transform player;
    Vector3 camoffset;

    private void Start()
    {
        camoffset.y = 15f;
    }
    void FixedUpdate()
    {
        cam.MovePosition(player.position + camoffset);
    }
}
