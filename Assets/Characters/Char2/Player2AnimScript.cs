using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2AnimScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal2") > 0 || Input.GetAxis("Vertical2") > 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
}
