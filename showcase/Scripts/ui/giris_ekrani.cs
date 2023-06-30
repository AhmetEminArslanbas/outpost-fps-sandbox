using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giris_ekrani : MonoBehaviour
{
    public GameObject baslangic_bilgi;

    public Mouse_control mouse_control;
    public player_controler player_controler;

    private void Start()
    {
        mouse_control.mouse_hareket_edebilir = false;
        player_controler.hareket_edebilir = false;
        baslangic_bilgi.gameObject.SetActive(true);
    }

    public void oyuna_basla()
    {
        baslangic_bilgi.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        mouse_control.mouse_hareket_edebilir = true;
        player_controler.hareket_edebilir = true;
    }
}
