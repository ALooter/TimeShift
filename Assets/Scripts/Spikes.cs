using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    public bool activated = false;
    Collider Collider;
    // Start is called before the first frame update
    void Activate()
    {
        activated = true;
        Collider.enabled = true;
    }
    void Start()
    {
        Collider = GetComponent<Collider>();
        Collider.enabled = false;
        Invoke("Activate", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
