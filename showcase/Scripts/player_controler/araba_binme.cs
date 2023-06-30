using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class araba_binme : MonoBehaviour
{
    GameObject player;
    GameObject araba;
    Camera camera_;

    public float arabaya_gecis_suresi;
    public bool arabaya_bindi;
    public bool arabaya_gec;

    Vector3 deneme;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        camera_ = Camera.main;
    }

    private void Update()
    {
        if (player_controler.envanter_slot_sellec == 3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, 6f, LayerMask.NameToLayer("araba")))
                {
                    Debug.DrawRay(camera_.transform.position, camera_.transform.forward, Color.red);
                    araba = hit.collider.gameObject;
                    deneme = hit.point;
                    arabaya_gec = true;
                }
            }
        }

        if(arabaya_gec)
        {
            arabaya_bin(araba, deneme);
        }
    }

    void arabaya_bin(GameObject araba, Vector3 deneme)
    {
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<SphereCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player_controler.hareket_edebilir = false;
        player.transform.position = Vector3.Lerp(player.transform.position, deneme, arabaya_gecis_suresi * Time.deltaTime);
        if (arabaya_bindi == false)
        {
            

            araba_control.araba_hareket = true;
            arabaya_bindi = true;
        }
        else
        {

            araba_control.araba_hareket = false;
            arabaya_bindi = false;
        }
    }
}
