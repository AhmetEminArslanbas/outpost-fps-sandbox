using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class dusman_controler : MonoBehaviour
{
    public player_controler player_controler;

    NavMeshAgent navmesh;
    Animator anim;
    raze_boom_bot raze_boom_bot;

    public GameObject player;

    public float playerdan_alinan_hasar;

    public float min_gorus_mesafesi;
    public float saldirma_mesafesi;
    public float mesafe;
    public float aci;
    public bool arada_obje_var_mi;

    public float dusman_can = 100;

    public bool oldu;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        player_controler = player.GetComponent<player_controler>();
        navmesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(!oldu)
        {
            mesafe = Vector3.Distance(player.transform.position, gameObject.transform.position);

            if (min_gorus_mesafesi == mesafe)
            {
                anim.SetBool("yuruyor", true);
                navmesh.isStopped = false;
                target_bak();
                navmesh.SetDestination(player.transform.position);

            }
            else
            {
                Vector3 fark = player.transform.position - gameObject.transform.position;
                Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
                Debug.DrawLine(transform.position, player.transform.position);
                aci = Vector3.Angle(transform.forward, fark);
                arada_obje_var_mi = Physics.Linecast(transform.position + Vector3.up * 2, player.transform.position + Vector3.up * 2, LayerMask.NameToLayer("Default"));// hiçbir işe yaramıyor
                Debug.Log(arada_obje_var_mi);
                if (aci <= 60 && !arada_obje_var_mi)
                {
                    Debug.Log("dusman takip ediyor");
                    anim.SetBool("yuruyor", true);
                    navmesh.isStopped = false;
                    target_bak();
                    navmesh.SetDestination(player.transform.position);
                }
                else if (aci > 60)
                {
                    Debug.Log("dusman durdu araya obje girdi");
                    //anim.SetBool("yuruyor", false);
                    //navmesh.isStopped = true;
                    gorus_alanindan_kacti();
                }
            }
            if (saldirma_mesafesi >= mesafe)
            {
                anim.SetBool("saldir", true);
                target_bak();
                navmesh.isStopped = true;
            }
            else
            {
                anim.SetBool("saldir", false);
            }
        }
    }
    public void hasar_al()
    {
        dusman_can -= playerdan_alinan_hasar;
        if(dusman_can <= 0)
        {
            anim.SetBool("oldu", true);
            dusman_can = 0;
            gameObject.GetComponent<Collider>().enabled = false;
            oldu = true;
            navmesh.isStopped = true;
            Destroy(gameObject, 3);
        }
    }

    public void hasar_ver()
    {
        player_controler.hasar_al();
    }

    void gorus_alanindan_kacti()
    {
        float timer = 0;
        target_bak();
        while (timer <= 5)
        {
            timer += Time.deltaTime;
            anim.SetBool("yuruyor", true);
            navmesh.isStopped = false;
            navmesh.SetDestination(player.transform.position);
        }
        anim.SetBool("yuruyor", false);
        navmesh.isStopped = true;
    }

    void target_bak()
    {
        Vector3 temp = player.transform.position;
        temp.y = transform.position.y;
        transform.LookAt(temp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hasar_al")
        {
            raze_boom_bot = GameObject.FindGameObjectWithTag("raze_boom_bot").GetComponent<raze_boom_bot>();
            if (raze_boom_bot.hasar_alinabilir)
            {
                hasar_al();
            }
        }
    }
}
