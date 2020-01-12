using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaAutoFire : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody projectile;
    GameObject ballista;
    Collider Collider;
    public bool activated = false;
    private WorldControl WorldControlScript;
    private PlayerControll PlayerControllScript;
    public bool check = false;
    public int[,] TimeCoordinates = new int[50, 2];
    int i;
    // Start is called before the first frame update
    void Fire()
    {
        Rigidbody clone;
        clone = Instantiate(projectile, transform.position + transform.TransformDirection(Vector3.forward) * 1f, transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        activated = false;
        TimeCoordinates[1, 0] = 1;
    }
    void Activate()
    {
        activated = true;
        Collider.enabled = true;
    }
    void Ray()
    {
        
    }
    void TimeCapture()
    {
        for (i = 49; i > 0; i--)
        {
            TimeCoordinates[i, 0] = TimeCoordinates[i - 1, 0];
        }

        for (i = 49; i > 0; i--)
        {
            TimeCoordinates[i, 1] = TimeCoordinates[i - 1, 1];
        }
    }
    void Start()
    {
        Collider = GetComponent<Collider>();
        Collider.enabled = false;
        Invoke("Activate", 2f);
        for (i = 1; i < 50; i++)
        {
            TimeCoordinates[i, 0] = 0;
        }
        for (i = 1; i < 50; i++)
        {
            TimeCoordinates[i, 1] = 0;
        }
        TimeCoordinates[0, 0] = 1;
        InvokeRepeating("TimeCapture", 0f, 0.1f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeCoordinates[0, 0] = 1;
        WorldControlScript = GameObject.Find("WorldController").GetComponent<WorldControl>();
        check = WorldControlScript.ZaWarudo;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        
        //int LayerMask = 1 << 11;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {if (hit.collider.tag == "Player")
            {
                if (activated == true)
                    if (WorldControlScript.ZaWarudo == false)
                    {
                        {
                            Invoke("Fire", 0f);
                            
                        }
                    }
            }
        }
        PlayerControllScript = GameObject.Find("PlayerCube").GetComponent<PlayerControll>();
        if (PlayerControllScript.TimeShift == true)
        {
            if (TimeCoordinates[49, 0] == 0)
            {
                Destroy(gameObject);
            }
            if (TimeCoordinates[49, 1] == 1)
            {
                activated = true;
            }
        }
    }
}
