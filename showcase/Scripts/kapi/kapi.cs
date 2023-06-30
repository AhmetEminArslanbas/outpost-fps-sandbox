using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kapi : MonoBehaviour
{
    Camera camera_;

    public LayerMask layerMask_;


    public Text bilgi_text;



    public float acilis_gecikmesi;

    public int acilis_derecesi;
    public int kapi_sifre;
    public bool kapi_kilitli_mi;
    public bool kapi_acik;


    private void Awake()
    {
        camera_ = Camera.main;
        if(kapi_sifre != 0)
        {
            kapi_kilitli_mi = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, 5f, layerMask_))
            {
                if (kapi_kilitli_mi)
                {
                    bilgi_text.gameObject.SetActive(true);
                    bilgi_text.text = "Kapı kilitli lütfen şifreyi giriniz.";
                    StartCoroutine(text_sil(bilgi_text, 3));
                }
                else
                {
                    kapi_acik = !kapi_acik;
                }
            }
            
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            kapi_acik = false;
        }
        if(kapi_acik == true)
        {
            //transform.eulerAngles = new Vector3(-90, 0, 120);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 0, acilis_derecesi), acilis_gecikmesi * Time.deltaTime);
        }
        else
        {
            //transform.eulerAngles = new Vector3(-90, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 0, 0), acilis_gecikmesi * Time.deltaTime);
        }
    }

    IEnumerator text_sil(Text text, int saniye)
    {
        yield return new WaitForSeconds(saniye);
        text.text = "";
        text.gameObject.SetActive(false);
    }
}
