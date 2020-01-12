using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
	Rigidbody rb;
	public float freezeDuration = 5;
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
    	if (other.gameObject.tag == "FreezeMine")
    	{

    		StartCoroutine(FreezePlayer());

    	}

	}

    IEnumerator FreezePlayer()
  		{
  			rb = gameObject.GetComponent<Rigidbody>();
    		rb.isKinematic = true;
    		yield return new WaitForSeconds(freezeDuration);
    		rb.isKinematic = false;
    	}
}
