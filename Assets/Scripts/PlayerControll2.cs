using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControll2 : MonoBehaviour
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

    public float playerspeed;

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


    //pauseui
    public Canvas pausecanvas;


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

            if (WorldControlScript.ZaWarudo1 == false)
            {
                OnHitDestroy bolt = collision.gameObject.GetComponent<OnHitDestroy>();
                stun = true;
                rb.velocity = bolt.rb.velocity;
                Invoke("StunOver", 2f);
            }
        }

        //mineknockback
        if (collision.gameObject.tag == "Mine")
        {
            Vector3 pushDIrection = collision.transform.position - transform.position;
            pushDIrection = -pushDIrection.normalized;
            GetComponent<Rigidbody>().AddForce(pushDIrection * minepushforce * 1000 * Time.deltaTime, ForceMode.VelocityChange);
            Destroy(collision.gameObject);
        }
        //spikesfreeze
        if (collision.gameObject.tag == "Spikes")
        {
            StartCoroutine(FreezePlayer());
            Destroy(collision.gameObject);
            frostimg.enabled = true;
            frostcd = 1f;
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

    IEnumerator FreezePlayer()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        yield return new WaitForSeconds(freezeDuration);
        rb.isKinematic = false;
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
    }
    void FixedUpdate()
    {
        //placemode 
        if (placemode == true)
        {

        }
        WorldControlScript = GameObject.Find("WorldController").GetComponent<WorldControl>();
        TimeCoordinates[0, 0] = transform.position.x;
        TimeCoordinates[0, 1] = transform.position.z;
        TimeCoordinates[0, 2] = transform.rotation.y;
        if (stun == false)
        {
            //placemode trigger
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //placecontainter = placemode;
                rb.velocity = new Vector3(0, 0, 0);
                placemode = !placemode;
                objectLineRenderer.enabled = !objectLineRenderer.enabled;
            }

            //rb.velocity = new Vector3(Input.GetAxis("Horizontal") * 2, 0, Input.GetAxis("Vertical") * 2);
            if (placemode == false)
            {
                if (Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.0f || Mathf.Abs(Input.GetAxis("Vertical2")) > 0.0f)
                {
                    rb.velocity = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
                    rb.rotation = Quaternion.Euler(new Vector3(0, (Mathf.Atan2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")) * Mathf.Rad2Deg), 0));
                    rotation = transform.localEulerAngles.y;

                }
                if (Mathf.Abs(Input.GetAxis("Horizontal2")) == 0.0f && Mathf.Abs(Input.GetAxis("Vertical2")) == 0.0f)
                {
                    transform.localEulerAngles = new Vector3(0, rotation, 0);

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
                rb.rotation = Quaternion.Euler(new Vector3(0, (Mathf.Atan2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")) * Mathf.Rad2Deg), 0));
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) == true && placemode == true)
            {

                Rigidbody clone;
                clone = Instantiate(trap1, transform.position, transform.rotation);


            }
            if (Input.GetKeyDown("1") && kapkancd <= 0)
            {
                kapkancd = kapkanmaxcd;

            }

            if (Input.GetKeyDown("2") && minecd <= 0)
            {
                minecd = minemaxcd;
                Vector3 mineoffset;
                mineoffset.x = Input.GetAxisRaw("Horizontal2");
                mineoffset.y = 0f;
                mineoffset.z = Input.GetAxisRaw("Vertical2");
                Rigidbody mine_clone;
                mine_clone = Instantiate(mineprefab, transform.position + 3 * mineoffset, transform.rotation);
            }

            if (Input.GetKeyDown("3") && spikescd <= 0)
            {
                spikescd = spikesmaxcd;
                Vector3 spikesoffset;
                spikesoffset.x = Input.GetAxisRaw("Horizontal2");
                spikesoffset.y = 0f;
                spikesoffset.z = Input.GetAxisRaw("Vertical2");
                Rigidbody mine_clone;
                mine_clone = Instantiate(spikesprefab, transform.position + 3 * spikesoffset, transform.rotation);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausecanvas.enabled = true;
            }
        }



        TimeShift = Input.GetKey("e");
        if (TimeShift == true)
        {
            timeshiftcd = timeshiftmaxcd;
            if (stun == true)
            {
                Invoke("StunOver", 0f);
            }
            stun = true;
            StartCoroutine(Tracer());
            stun = false;
        }
        ZaWarudo = Input.GetKey("x");
        if (ZaWarudo == true)
        {
            zawarudocd = zawarudomaxcd;
            playerlight.color = Color.blue;
            if (stun == true)
            {
                Invoke("StunOver", 0f);
            }
        }


    }
}
