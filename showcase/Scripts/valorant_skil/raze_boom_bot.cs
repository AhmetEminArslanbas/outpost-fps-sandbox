using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raze_boom_bot : MonoBehaviour
{
    player_controler player_control;
    GameObject[] hedef_obje;
    GameObject saldirilan_hedef;

    Rigidbody rb;

    public float hiz;
    public float max_aci;
    public float min_mesafe;
    float en_yakin_mesafe = 100;

    [SerializeField] float aci;
    [SerializeField] bool duvar_var_mi;

    public bool hasar_alinabilir;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player_control = GameObject.FindGameObjectWithTag("player").GetComponent<player_controler>();

        Destroy(gameObject, 15);
    }

    private void FixedUpdate()
    {
        transform.Translate(0, 0, hiz * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Untagged")
        {
            transform.rotation = Quaternion.LookRotation(Vector3.Reflect(transform.forward, collision.contacts[0].normal));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "dusman")
        {
            hedef_obje = GameObject.FindGameObjectsWithTag("dusman");

            for(int i = 0; i < hedef_obje.Length; i++)
            {
                float mesafe = Vector3.Distance(transform.position, hedef_obje[i].transform.position);
                if(en_yakin_mesafe > mesafe)
                {
                    en_yakin_mesafe = mesafe;
                    saldirilan_hedef = hedef_obje[i];
                }
            }

            Vector3 fark = saldirilan_hedef.transform.position - transform.position;
            aci = Vector3.Angle(transform.forward, fark);
            RaycastHit hit;
            duvar_var_mi = Physics.Linecast(transform.position + Vector3.up, saldirilan_hedef.transform.position + Vector3.up * 2, out hit);
            if (!duvar_var_mi && aci < max_aci && saldirilan_hedef.GetComponent<dusman_controler>().oldu == false)
            {
                transform.LookAt(saldirilan_hedef.transform.position);
                hiz = 5;
                float mesafe = Vector3.Distance(transform.position, saldirilan_hedef.transform.position);
                if (mesafe < min_mesafe)
                {
                    hasar_alinabilir = true;
                    transform.Translate(0, 0.1f, 0);
                    Destroy(gameObject, 0.2f);
                }
            }
            else
            {
                hiz = 3;
            }

        }
    }
}
