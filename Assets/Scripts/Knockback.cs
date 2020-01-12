using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	public float force = 1F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
    	if (other.gameObject.tag == "Mine")
    	{
    		Vector3 pushDIrection = other.transform.position - transform.position;
    		pushDIrection = -pushDIrection.normalized;
    		GetComponent<Rigidbody>().AddForce(pushDIrection * force * 100, ForceMode.Impulse);
    	}
    }
}
