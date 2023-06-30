using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_controler : MonoBehaviour
{
    public GameObject silah;
    public GameObject dusman; //vurulan dusmanın kendisini dusman a eşitle ve hasar al fonksiyonunu çalıştır
    public GameObject dusman_prefab;//look at i z de sıfırla

    public GameObject raze_boom_bot_prefab;
    

    public LayerMask layer_mask;
    public Text can_text;

    public float hasar_verme_araligi;
    public float player_can = 100;
    public float dusmandan_alinan_hasar;
    public static bool hareket_edebilir = true;

    public static int envanter_slot_sellec;

    public int sarjor;
    public int cephane;
    public int sarjor_kapasite;

    float horizontal;
    float vertical;
    bool aralik = true;
    bool spam_onleyici;

    Camera camera_;
    Rigidbody rb;
    Animator anim;

    [SerializeField] float hiz;
    [SerializeField] float jump_hiz;
    float jump;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        camera_ = Camera.main;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject dusman = Instantiate(dusman_prefab, new Vector3(100, 0, 40), Quaternion.identity);
            this.dusman = dusman;
        }

        #region sayi_tusu_atamalari
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            envanter_slot_sellec = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            envanter_slot_sellec = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            envanter_slot_sellec = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            envanter_slot_sellec = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            envanter_slot_sellec = 5;
        }
        #endregion


        if (Input.GetKey(KeyCode.Space))
        {
            jump = jump_hiz;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontal = Input.GetAxis("Horizontal") * 2;
            vertical = Input.GetAxis("Vertical") * 2;
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        if (Input.GetMouseButton(0))
        {
            if(envanter_slot_sellec == 1)
            {
                if (hareket_edebilir == true)
                {
                    anim.SetBool("ates_et", true);
                    ates_et();
                }
            }
        }
        else
        {
            anim.SetBool("ates_et", false);
        }
        if(hareket_edebilir == false)
        {
            anim.SetBool("ates_et", false);
            anim.SetFloat("horizontal", 0);
            anim.SetFloat("vertical", 0);
        }


        if (envanter_slot_sellec == 4)
        {
            if (Input.GetMouseButtonDown(0) && !spam_onleyici)
            {
                Instantiate(raze_boom_bot_prefab, transform.position + Vector3.forward, transform.rotation);
                spam_onleyici = true;
                StartCoroutine(spam_onle());
            }
        }
    }
    void FixedUpdate()
    {
        if(hareket_edebilir == true)
        {
            hareket();
        }
    }

    void hareket()
    {

        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("vertical", vertical);
        /*
        if(horizontal != 0 || vertical !=0)
        {
            silah.transform.localPosition = new Vector3(-0.0078f, 0.2391f, 0.0338f);
            silah.transform.eulerAngles = new Vector3(-432.166f, -291.822f, 38.553f);
        }
        else if (horizontal == 0 || vertical == 0)
        {
            silah.transform.localPosition = new Vector3(-0.063f, 0.225f, 0.004f);
            silah.transform.eulerAngles = new Vector3(-102.353f, -0.9129944f, 89.662f);
        }*/

        rb.velocity = transform.TransformDirection(new Vector3(horizontal * hiz * Time.deltaTime, rb.velocity.y + jump * Time.deltaTime, vertical * hiz * Time.deltaTime));
        //transform.Translate(new Vector3(horizontal * hiz * Time.deltaTime, rb.velocity.y, vertical * hiz * Time.deltaTime));
    }

    void ates_et()
    {
        //Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(camera_.transform.position, camera_.transform.forward * hit.distance, Color.red);
            if (hit.collider.gameObject.tag == "dusman")
            {
                Debug.Log("dusmana vuruldu");
            }
            if (aralik == true && hit.collider.gameObject.tag == "dusman")
            {
                aralik = false;
                StartCoroutine(hasar_alma_araligi(hasar_verme_araligi));
            }
        }
    }
    IEnumerator hasar_alma_araligi(float saniye)
    {
        if(dusman != null)
        {
            Debug.Log("dusmana hasar verildi");
            dusman.GetComponent<dusman_controler>().hasar_al();
        }
        yield return new WaitForSeconds(saniye);
        aralik = true;
    }
    IEnumerator spam_onle()
    {
        yield return new WaitForSeconds(0.2f);
        spam_onleyici = false;
    }


    public void hasar_al()
    {
        player_can -= dusmandan_alinan_hasar;
        if (player_can <= 0)
        {
            anim.SetBool("oldu", true);
            can_text.text = "0";
            Destroy(gameObject, 3);// farklı yere taşıyıp ui ekranı göster
        }
        else
        {
            can_text.text = player_can.ToString();
        }
        
    }
}
