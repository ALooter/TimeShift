using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDestroy : MonoBehaviour
{
    public bool ricochet = false;
    public Rigidbody rb;
    GameObject projectile;
    private WorldControl WorldControlScript;
    private PlayerControll PlayerControllScript;
    public Vector3 startPosition;
    public float distanceMoved;
    
    void Destroy()
    {
        
            Destroy(gameObject);
        
    }
    void Ricochet()
    {
        ricochet = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            ContactPoint contact = collision.contacts[0];
            if (ricochet == true)
            {
                if (WorldControlScript.ZaWarudo == false)
                {
                    Invoke("Destroy", 0);
                }
            }
            else if (ricochet == false)
            {
                //rb.velocity = 
                transform.forward = Vector3.Reflect(rb.velocity, contact.normal);
                Invoke("Ricochet", 0.5f);
            }
        }
        if (collision.gameObject.tag == "Player")
        { 
            if (WorldControlScript.ZaWarudo == false)
            {
                Invoke("Destroy", 0);
            }
    }
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.TransformDirection(Vector3.forward * 10);
        WorldControlScript = GameObject.Find("WorldController").GetComponent<WorldControl>();
        PlayerControllScript = GameObject.Find("PlayerCube").GetComponent<PlayerControll>();
        if (WorldControlScript.ZaWarudo == true)
        {
            rb.velocity = rb.velocity * 0;
        }
        
        distanceMoved += Vector3.Distance(transform.position, startPosition);
        if (distanceMoved > 50000f)
        {
            Invoke("Destroy", 0);
        }
        if ( PlayerControllScript.TimeShift == true)
        {
            Invoke("Destroy", 0);
        }
    }
}
