using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obje_bakis : MonoBehaviour
{
    public Transform hedef_transform;
    Camera camera_;
    Vector3 original_pos;
    Vector3 original_rot;
    GameObject obje_gameobje;

    public LayerMask layer_mask;

    player_controler player_controler;
    public Mouse_control mouse_control;


    public float aralik_saniye;
    bool nesne_elinde_mi;

    public float hassasiyet;
    public float mouse_x;
    public float mouse_y;


    private void Awake()
    {
        player_controler = GetComponent<player_controler>();
        camera_ = Camera.main;
    }

    private void Update()
    {
        if(player_controler.envanter_slot_sellec == 3)
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, 4f, layer_mask) && nesne_elinde_mi == false)
                {
                    original_pos = hit.transform.position;
                    original_rot = hit.transform.eulerAngles;
                    obje_gameobje = hit.collider.gameObject;

                    hit.collider.enabled = false;

                    player_controler.hareket_edebilir = false;
                    mouse_control.mouse_hareket_edebilir = false;

                    nesne_elinde_mi = true;
                    Debug.Log("eşitledi");
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                player_controler.hareket_edebilir = true;
                mouse_control.mouse_hareket_edebilir = true;

                nesne_elinde_mi = false;
                
                StartCoroutine(obje_eski_yerinde());
            }

            if(nesne_elinde_mi)
            {
                obje_gameobje.transform.position = Vector3.Lerp(obje_gameobje.transform.position, hedef_transform.position, aralik_saniye * Time.deltaTime);

                mouse_x += Input.GetAxis("Mouse X") * hassasiyet;
                mouse_y += Input.GetAxis("Mouse Y") * hassasiyet;
                obje_gameobje.transform.eulerAngles = new Vector3(obje_gameobje.transform.rotation.x + -mouse_y, obje_gameobje.transform.rotation.y + -mouse_x, 0);
            }
            else
            {
                if(obje_gameobje != null)
                {
                    obje_gameobje.transform.position = Vector3.Lerp(obje_gameobje.transform.position, original_pos, aralik_saniye * Time.deltaTime);
                    obje_gameobje.transform.eulerAngles = Vector3.Lerp(obje_gameobje.transform.eulerAngles, original_rot, aralik_saniye * Time.deltaTime);
                }
            }
        }
    }

    IEnumerator obje_eski_yerinde()
    {
        yield return new WaitForSeconds(aralik_saniye - 0.25f);
        obje_gameobje.transform.eulerAngles = original_rot;
        obje_gameobje.GetComponent<Collider>().enabled = true;

    }
}
