using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControl : MonoBehaviour
{
    public bool PlayerOneWorld = false;
    public bool PlayerOneCD = false;
    public bool ZaWarudo1 = false;
    private PlayerControll PlayerOneScript;
    private PlayerControll2 PlayerTwoScript;
    public bool PlayerTwoWorld = false;
    public bool PlayerTwoCD = false;
    public bool ZaWarudo2 = false;
   // private PlayerControll PlayerTwoScript;
    void PlayerOneWorldControl()
    {
        ZaWarudo1 = false;
    }
    void PlayerOneWorldCD()
    {
        PlayerOneCD = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerOneScript = GameObject.Find("PlayerCube1").GetComponent<PlayerControll>();
        PlayerOneWorld = PlayerOneScript.ZaWarudo;
        PlayerTwoScript = GameObject.Find("PlayerCube2").GetComponent<PlayerControll2>();
        PlayerTwoWorld = PlayerOneScript.ZaWarudo;

        if (PlayerOneWorld == true || PlayerTwoWorld == true)
            if (PlayerOneCD == false)
            {
                {
                    ZaWarudo1 = true;
                    PlayerOneCD = true;
                    Invoke("PlayerOneWorldControl", 5f);
                    Invoke("PlayerOneWorldCD", 10f);
                }
            }
    }
}
