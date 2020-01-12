using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerPos : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 mousePosition;
    public Rigidbody playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
             mousePosition.x = hit.point.x;
             mousePosition.z = hit.point.z;
        }
        mousePosition.y = transform.position.y;
        transform.LookAt(mousePosition);
        rb.position = playerPosition.position;
    }
}
