﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanagerscript : MonoBehaviour
{

    /*public static AudioClip balistaplacedsfx, balistadamagesfx, minedamagesfx, spikesdamagesfx, explosionsfx, timeshiftsfx, zawarudosfx;
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

        audiosrc = GetComponent<AudioSource>();
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
    }*/





    public AudioSource efxSource;                    
    public AudioSource musicSource;                    

    //Used to play single sound clips.
    public void PlaySFX(AudioClip clip)
    {
        
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    /*public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }
}*/
}
