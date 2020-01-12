using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTrap : MonoBehaviour
{
    public bool inWall = false;
    public Rigidbody trap1;
    public float x;
    public float y;
    public float z;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        inWall = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inWall = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        if (Input.GetKeyDown(KeyCode.Mouse0) == true){
            if (inWall == false)
            {
                Rigidbody clone;
                clone = Instantiate(trap1, new Vector3(x, y, z), transform.rotation);
                
            }
        }
    }
}
