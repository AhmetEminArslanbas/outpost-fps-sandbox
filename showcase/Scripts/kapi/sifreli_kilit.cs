using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sifreli_kilit : MonoBehaviour
{
    public GameObject sifre_paneli;

    public kapi kapi_controler;
    public player_controler player_controler;
    public Mouse_control mouse_control;
    Camera camera_;

    public Text bilgi_text;
    public InputField field;

    public LayerMask layerMask_;

    private void Awake()
    {
        camera_ = Camera.main;
        sifre_paneli.gameObject.SetActive(false);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, 5f, layerMask_))
            {
                if (kapi_controler.kapi_kilitli_mi == true)
                {
                    panel_acildi();
                }
                else
                {
                    panel_acildi();
                    /*
                    bilgi_text.gameObject.SetActive(true);
                    bilgi_text.text = "Kapı kilitli değil.";
                    StartCoroutine(text_sil(bilgi_text, 3));*/
                }
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            panel_kapandi();
        }
    }
    void panel_acildi()
    {
        Cursor.lockState = CursorLockMode.None;
        mouse_control.mouse_hareket_edebilir = false;
        player_controler.hareket_edebilir = false;
        sifre_paneli.gameObject.SetActive(true);
    }
    void panel_kapandi()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouse_control.mouse_hareket_edebilir = true;
        player_controler.hareket_edebilir = true;
        sifre_paneli.gameObject.SetActive(false);
    }
    IEnumerator text_sil(Text text, int saniye)
    {
        yield return new WaitForSeconds(saniye);
        text.text = "";
        text.gameObject.SetActive(false);
    }

    #region butonlari_tanimlama

    public void buton_(int deger)
    {
        if(field.text.Length < 4)
        {
            field.text += deger;
        }
    }


    public void del_buton()
    {
        field.text = "";
        bilgi_text.gameObject.SetActive(true);
        bilgi_text.text = "Pin silindi";
        StartCoroutine(text_sil(bilgi_text, 2));
    }
    public void check_buton()
    {
        if(kapi_controler.kapi_kilitli_mi == true)
        {
            if (kapi_controler.kapi_sifre.ToString() == field.text)
            {
                kapi_controler.kapi_sifre = 0;
                kapi_controler.kapi_kilitli_mi = false;
                field.text = "";
                panel_kapandi();
                bilgi_text.gameObject.SetActive(true);
                bilgi_text.text = "Şifre resetlendi";
                StartCoroutine(text_sil(bilgi_text, 2));
            }
            else
            {
                field.text = "";
                bilgi_text.gameObject.SetActive(true);
                bilgi_text.text = "Şifre yanlış";
                StartCoroutine(text_sil(bilgi_text, 2));
            }
        }
        else
        {
            if(field.text.Length == 4)
            {
                kapi_controler.kapi_sifre = int.Parse(field.text);
                kapi_controler.kapi_kilitli_mi = true;
                field.text = "";
                panel_kapandi();
                bilgi_text.gameObject.SetActive(true);
                bilgi_text.text = "Şifre oluşturuldu";
                StartCoroutine(text_sil(bilgi_text, 2));
            }
        }
    }

    #endregion
}
