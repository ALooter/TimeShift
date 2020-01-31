using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControll : MonoBehaviour
{
    public Rigidbody rb;
    public bool Left = false;
    public bool Right = false;
    public bool Forward = false;
    public bool Back = false;
    public float x = 0;
    public float z = 0;
    public Vector3 LR;
    public Vector3 FB;
    public Rigidbody trap1;
    public GameObject playerone;
    public bool stun = false;
    public bool TimeShift = false;
    public bool ZaWarudo = false;
    public Vector3 Ricochet;
    private WorldControl WorldControlScript;
    public float[,] TimeCoordinates = new float[50, 3];
    int i;
    int a;
    public bool placemode = false;
    private LineRenderer objectLineRenderer;
    public float rotation;
    public int HP = 3;
    public float playerspeed;
    private PlayerControll2 PlayerControll2;

    //speed_coef - movement speed is multiplyed by this value (easy way to make hero faster/slower)
    public float speed_coef;

    //abilities cooldowns
    public float zawarudomaxcd = 10f;
    public float zawarudocd = 0f;

    public float balistamaxcd = 10f;
    public float balistacd = 0f;

    public float timeshiftmaxcd = 15f;
    public float timeshiftcd = 0f;

    public float kapkanmaxcd = 15f;
    public float kapkancd = 0f;

    public float spikesmaxcd = 15f;
    public float spikescd = 0f;

    public float minemaxcd = 15f;
    public float minecd = 0f;

    //abilities ui
    public Image balistaui;
    public Image zawarudoui;
    public Image timeshiftui;
    public Image kapkanui;
    public Image spikesui;
    public Image mineui;

    //light
    public Light playerlight;
    Color playerlightcolor;

    //frost
    public Image frostimg;
    public float frostcd = 0f;

    //mine
    public float minepushforce = 3f;
    public Rigidbody mineprefab;

    //spikes
    public float freezeDuration = 2f;
    public Rigidbody spikesprefab;

    //hp ui
    public Image heart1;
    public Image heart2;
    public Image heart3;

    //pauseui
    public Canvas pausecanvas;


    //---audio---
    public AudioClip balistaplacedsfx, balistadamagesfx, minedamagesfx, spikesdamagesfx, explosionsfx, timeshiftsfx, zawarudosfx;
    //public static AudioSource audiosrc;
    public AudioSource sfxSource;

    public void PlaySFX(AudioClip clip)
    {

        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        sfxSource.clip = clip;

        //Play the clip.
        sfxSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor")
        {
            ContactPoint contact = collision.contacts[0];
            if (stun == true)
            {
                //Ricochet = rb.velocity * -1;
                rb.velocity = Vector3.Reflect(rb.velocity, contact.normal);
                Invoke("StunOver", 0.5f);
            }
        }
        if (collision.gameObject.tag == "Bolt")
        {

            if (ZaWarudo == false)
            {
                OnHitDestroy bolt = collision.gameObject.GetComponent<OnHitDestroy>();
                stun = true;
                rb.velocity = bolt.rb.velocity;
                Invoke("StunOver", 2f);
                HP -= 1;
                PlaySFX(balistadamagesfx);
            }
        }

        //mineknockback
        if (collision.gameObject.tag == "Mine")
        {
            if (ZaWarudo == false && collision.gameObject.GetComponent<Mine>().activated == true)
            {
                Vector3 pushDIrection = collision.transform.position - transform.position;
                pushDIrection = -pushDIrection.normalized;
                GetComponent<Rigidbody>().AddForce(pushDIrection * minepushforce * 1000 * Time.deltaTime, ForceMode.VelocityChange);
                Destroy(collision.gameObject);
                HP -= 1;
                PlaySFX(minedamagesfx);
            }
        }
        //spikesfreeze
        if (collision.gameObject.tag == "Spikes")
        {
            if (ZaWarudo == false && collision.gameObject.GetComponent<Spikes>().activated == true)
            {
                StartCoroutine(FreezePlayer());
                Destroy(collision.gameObject);
                frostimg.enabled = true;
                frostcd = 1f;
                HP -= 1;
                PlaySFX(spikesdamagesfx);
            }
        }
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
     
        for (i = 49; i > 0; i--)
        {
            TimeCoordinates[i, 2] = TimeCoordinates[i - 1, 2];
        }



    }
    void StunOver()
    {
        if (stun == true)
        {
            stun = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
        

    }
    IEnumerator Tracer()
    {
        for (a = 0; a < 50; a++)
        {
            transform.position = new Vector3(TimeCoordinates[a, 0], transform.position.y, TimeCoordinates[a, 1]);
            transform.rotation = Quaternion.Euler(0, TimeCoordinates[a, 2], 0);
            yield return new WaitForSeconds(0.001f);
        }
    }
    void Death()
    {

    }
    IEnumerator FreezePlayer()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        yield return new WaitForSeconds(freezeDuration);
        rb.isKinematic = false;
    }
    void PlayerWorldControl()
    {
        ZaWarudo = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (i = 0; i < 50; i++)
        {
            TimeCoordinates[i, 0] = transform.position.x;
        }
        for (i = 0; i < 50; i++)
        {
            TimeCoordinates[i, 1] = transform.position.z;
        }
        for (i = 0; i < 50; i++)
        {
            TimeCoordinates[i, 2] = transform.rotation.y;
        }
        InvokeRepeating("TimeCapture", 0f, 0.1f);

        //pauseui
        pausecanvas.enabled = false;

        //light
        playerlightcolor = playerlight.color;
        //line renderer
        objectLineRenderer = rb.GetComponent<LineRenderer>();
        objectLineRenderer.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        balistacd -= Time.fixedDeltaTime;
        zawarudocd -= Time.fixedDeltaTime;
        timeshiftcd -= Time.fixedDeltaTime;
        kapkancd -= Time.fixedDeltaTime;
        minecd -= Time.fixedDeltaTime;
        spikescd -= Time.fixedDeltaTime;

        frostcd -= Time.fixedDeltaTime;

        balistaui.fillAmount = balistacd / balistamaxcd;
        zawarudoui.fillAmount = zawarudocd / zawarudomaxcd;
        timeshiftui.fillAmount = timeshiftcd / timeshiftmaxcd;
        kapkanui.fillAmount = kapkancd / kapkanmaxcd;
        mineui.fillAmount = minecd / minemaxcd;
        spikesui.fillAmount = spikescd / spikesmaxcd;

        if (frostcd <= 0)
        {
            frostimg.enabled = false;
        }
        if (zawarudocd <= 5f)
        {
            playerlight.color = playerlightcolor;
        }

        if (HP == 3)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
        }
        if (HP == 2)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(false);
        }
        if (HP == 1)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        if (HP < 1)
        {
            Invoke("Death", 0f);
        }
        //placemode 
        PlayerControll2 = GameObject.Find("PlayerCube2").GetComponent<PlayerControll2>();
        WorldControlScript = GameObject.Find("WorldController").GetComponent<WorldControl>();
        TimeCoordinates[0, 0] = transform.position.x;
        TimeCoordinates[0, 1] = transform.position.z;
        TimeCoordinates[0, 2] = transform.rotation.y;
        if (PlayerControll2.ZaWarudo == true)
        {
            placemode = false;
        }
        if (stun == false)
        {
            if (ZaWarudo == true || (ZaWarudo == false && PlayerControll2.ZaWarudo == false))
            {
                //placemode trigger

                if (Input.GetButtonDown("Construction1"))

                {
                    //placecontainter = placemode;
                    rb.velocity = new Vector3(0, 0, 0);
                    placemode = !placemode;
                    objectLineRenderer.enabled = !objectLineRenderer.enabled;
                }

                //rb.velocity = new Vector3(Input.GetAxis("Horizontal") * 2, 0, Input.GetAxis("Vertical") * 2);
                if (placemode == false)
                {
                    if (Mathf.Abs(Input.GetAxis("Horizontal1")) > 0.0f || Mathf.Abs(Input.GetAxis("Vertical1")) > 0.0f)
                    {
                        rb.velocity = new Vector3(Input.GetAxis("Horizontal1") * speed_coef, 0, Input.GetAxis("Vertical1") * speed_coef);
                        rb.rotation = Quaternion.Euler(new Vector3(0, (Mathf.Atan2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")) * Mathf.Rad2Deg), 0));
                        rotation = transform.localEulerAngles.y;

                    }
                    if (Mathf.Abs(Input.GetAxis("Horizontal1")) <= 0.03f && Mathf.Abs(Input.GetAxis("Vertical1")) <= 0.03f)
                    {
                        transform.localEulerAngles = new Vector3(0, rotation, 0);
                        rb.velocity = new Vector3(0, 0, 0);
                    }

                    //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    // {

                    //     transform.forward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    //
                    //    rb.velocity = transform.forward * playerspeed;

                    // }
                    // if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                    // {
                    //     if (stun == false)
                    //    {
                    //        rb.velocity = new Vector3(0, 0, 0);
                    //   }
                    // }
                    //if (new Vector3())
                }
                if (placemode == true)
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, (Mathf.Atan2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")) * Mathf.Rad2Deg), 0));
                }


                if (Input.GetButtonDown("Balista1") == true && placemode == true && balistacd < 0f)
                {

                    Rigidbody clone;
                    clone = Instantiate(trap1, transform.position + new Vector3(0, 0, 2), transform.rotation);
                    balistacd = balistamaxcd;
                    PlaySFX(balistaplacedsfx);

                }
                if (Input.GetButtonDown("Kapkan1") && kapkancd <= 0)

                {
                    kapkancd = kapkanmaxcd;

                }


                if (Input.GetButtonDown("Mine1") && minecd <= 0)

                {
                    minecd = minemaxcd;
                    Vector3 mineoffset;
                    mineoffset.x = Input.GetAxisRaw("Horizontal1");
                    mineoffset.y = 0f;
                    mineoffset.z = Input.GetAxisRaw("Vertical1");
                    Rigidbody mine_clone;
                    mine_clone = Instantiate(mineprefab, transform.position + 3 * mineoffset, transform.rotation);

                }


                if (Input.GetButtonDown("Spikes1") && spikescd <= 0)

                {
                    spikescd = spikesmaxcd;
                    Vector3 spikesoffset;
                    spikesoffset.x = Input.GetAxisRaw("Horizontal1");
                    spikesoffset.y = 0f;
                    spikesoffset.z = Input.GetAxisRaw("Vertical1");
                    Rigidbody mine_clone;
                    mine_clone = Instantiate(spikesprefab, transform.position + 3 * spikesoffset, transform.rotation);
                }

                
            }



            TimeShift = Input.GetButton("Timeshift1");
            if (TimeShift == true && timeshiftcd < 0f)
            {
                timeshiftcd = timeshiftmaxcd;
                PlaySFX(timeshiftsfx);
                if (stun == true)
                {
                    Invoke("StunOver", 0f);
                }
                stun = true;
                StartCoroutine(Tracer());
                stun = false;
            }
        }
        ZaWarudo = Input.GetButton("Zawarudo1");
        if (ZaWarudo == true && zawarudocd < 0f)
        {
            zawarudocd = zawarudomaxcd;
            playerlight.color = Color.blue;
            PlaySFX(zawarudosfx);
            if (stun == true)
            {
                Invoke("StunOver", 0f);
            }
            ZaWarudo = true;
            Invoke("PlayerWorldControl", 5f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausecanvas.enabled = true;
        }

    }
}
