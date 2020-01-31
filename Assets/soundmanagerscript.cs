using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanagerscript : MonoBehaviour
{

    public static AudioClip balistaplacedsfx, balistadamagesfx, minedamagesfx, spikesdamagesfx, explosionsfx, timeshiftsfx, zawarudosfx;
    public static AudioClip balistaplacedsfx2, balistadamagesfx2, minedamagesfx2, spikesdamagesfx2, explosionsfx2, timeshiftsfx2, zawarudosfx2;
    static AudioSource audiosrc;

    
    void Start()
    {
        balistaplacedsfx = Resources.Load<AudioClip>("balistaplaced1");
        balistadamagesfx = Resources.Load<AudioClip>("balistadamagesfx");
        minedamagesfx = Resources.Load<AudioClip>("mine1");
        spikesdamagesfx = Resources.Load<AudioClip>("def");
        timeshiftsfx = Resources.Load<AudioClip>("podliva");
        zawarudosfx = Resources.Load<AudioClip>("zavar1");
        //2
        balistaplacedsfx2 = Resources.Load<AudioClip>("balistaplaced2");
        balistadamagesfx2 = Resources.Load<AudioClip>("balistadamage2");
        minedamagesfx2 = Resources.Load<AudioClip>("mine2");
        spikesdamagesfx2 = Resources.Load<AudioClip>("damag");
        timeshiftsfx2 = Resources.Load<AudioClip>("hrhrhr");
        zawarudosfx2 = Resources.Load<AudioClip>("zavar3");
    }

    
    public static void PlaySFX(string clip)
    {
        switch (clip)
        {
            case "balistaplacedsfx":
                audiosrc.PlayOneShot(balistaplacedsfx);
                break;
            case "balistadamagesfx":
                audiosrc.PlayOneShot(balistadamagesfx);
                break;
            case "minedamagesfx":
                audiosrc.PlayOneShot(minedamagesfx);
                break;
            case "spikesdamagesfx":
                audiosrc.PlayOneShot(spikesdamagesfx);
                break;
            case "explosionsfx":
                audiosrc.PlayOneShot(explosionsfx);
                break;
            case "timeshiftsfx":
                audiosrc.PlayOneShot(timeshiftsfx);
                break;
            case "zawarudosfx":
                audiosrc.PlayOneShot(zawarudosfx);
                break;
                //2
            case "balistaplacedsfx2":
                audiosrc.PlayOneShot(balistaplacedsfx2);
                break;
            case "balistadamagesfx2":
                audiosrc.PlayOneShot(balistadamagesfx2);
                break;
            case "minedamagesfx2":
                audiosrc.PlayOneShot(minedamagesfx2);
                break;
            case "spikesdamagesfx2":
                audiosrc.PlayOneShot(spikesdamagesfx2);
                break;
            case "explosionsfx2":
                audiosrc.PlayOneShot(explosionsfx2);
                break;
            case "timeshiftsfx2":
                audiosrc.PlayOneShot(timeshiftsfx2);
                break;
            case "zawarudosfx2":
                audiosrc.PlayOneShot(zawarudosfx2);
                break;
        }
    }
}
