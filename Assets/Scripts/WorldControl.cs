using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControl : MonoBehaviour
{
    public bool PlayerOneWorld = false;
    public bool PlayerOneCD = false;
    public bool ZaWarudo = false;
    private PlayerControll PlayerOneScript;
    void PlayerOneWorldControl()
    {
        ZaWarudo = false;
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
        PlayerOneScript = GameObject.Find("PlayerCube").GetComponent<PlayerControll>();
        PlayerOneWorld = PlayerOneScript.ZaWarudo;

        if (PlayerOneWorld == true)
            if (PlayerOneCD == false)
            {
                {
                    ZaWarudo = true;
                    PlayerOneCD = true;
                    Invoke("PlayerOneWorldControl", 5f);
                    Invoke("PlayerOneWorldCD", 10f);
                }
            }
    }
}
