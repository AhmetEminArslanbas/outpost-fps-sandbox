using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_controler : MonoBehaviour
{
    public GameObject temel;
    public GameObject temel_preview;
    public GameObject woll;
    public GameObject woll_preview;


    player_controler player_controler;
    Camera camera_;
    Transform soket;

    public LayerMask layer_mask;

    public int obje_secenegi;

    public bool tap_panel_acik;
    bool insa_edilebilir;

    private void Awake()
    {
        camera_ = Camera.main;
        player_controler = GetComponent<player_controler>();
    }

    private void Update()
    {
        if(player_controler.envanter_slot_sellec == 2)
        {
            if (insa_edilebilir)
            {
                temel_preview.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                woll_preview.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
            }
            else
            {
                temel_preview.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                woll_preview.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            }


            RaycastHit hit;
            if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, Mathf.Infinity, layer_mask))
            {
                if(obje_secenegi == -1)
                {
                    Debug.DrawRay(camera_.transform.position, camera_.transform.forward * hit.distance, Color.red);
                    woll_preview.transform.position = new Vector3(0, -500, 0);
                    if (hit.collider.gameObject.tag == "temel_soket")
                    {
                        insa_edilebilir = true;
                        soket = hit.transform;
                        temel_preview.transform.position = soket.position;
                        temel_spawnla(temel);
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "temel")
                        {
                            insa_edilebilir = false;
                        }
                        else
                        {
                            insa_edilebilir = true;
                            temel_preview.transform.position = hit.point;
                            temel_spawnla(temel);
                        }
                    }
                }
                else if(obje_secenegi == 1)
                {
                    temel_preview.transform.position = new Vector3(0, -500, 0);
                    if (hit.collider.gameObject.tag == "woll_soket")
                    {
                        insa_edilebilir = true;
                        soket = hit.transform;
                        woll_preview.transform.position = soket.position;
                        temel_spawnla(woll);
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "woll")
                        {
                            insa_edilebilir = false;
                        }
                        else
                        {
                            insa_edilebilir = true;
                            woll_preview.transform.position = hit.point;
                            temel_spawnla(woll);
                        }
                    }
                }
                /*if(Input.GetMouseButtonDown(1)) //temel kaldırma sistemini koy
                {
                    if(hit.collider.gameObject.tag == "temel")
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.gameObject.tag == "woll")
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }*/
            }
        }
        else
        {
            temel_preview.transform.position = new Vector3(0, -500, 0);
            woll_preview.transform.position = new Vector3(0, -500, 0);
        }
    }
    void temel_spawnla(GameObject obj)
    {
        if(!tap_panel_acik)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(obj, temel_preview.transform.position, temel_preview.transform.rotation);
            }
        }
    }
}
