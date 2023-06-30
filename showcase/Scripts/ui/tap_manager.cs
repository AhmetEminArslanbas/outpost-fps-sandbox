using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tap_manager : MonoBehaviour
{
    public GameObject tap_menu_image;
    public Texture[] resimler;

    public build_controler build_controler;
    public player_controler player_controler;

    public float mouse_x;
    public int secilen_yon;

    private void Awake()
    {
        tap_menu_image.GetComponent<RawImage>().texture = resimler[0];
        secilen_yon = 0;
        tap_menu_image.SetActive(false);
    }

    private void Update()
    {
        if(player_controler.envanter_slot_sellec == 2)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                build_controler.tap_panel_acik = true;
                tap_menu_image.SetActive(true);
                mouse_x = Input.GetAxis("Mouse X");
                if (mouse_x > 0)
                {
                    tap_menu_image.GetComponent<RawImage>().texture = resimler[2];
                    secilen_yon = 1;
                }
                else if (mouse_x < 0)
                {
                    tap_menu_image.GetComponent<RawImage>().texture = resimler[1];
                    secilen_yon = -1;
                }



                if (Input.GetMouseButtonDown(0))
                {
                    if (secilen_yon > 0)
                    {
                        build_controler.obje_secenegi = 1;
                    }
                    else if (secilen_yon < 0)
                    {
                        build_controler.obje_secenegi = -1;
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                build_controler.tap_panel_acik = false;
                tap_menu_image.GetComponent<RawImage>().texture = resimler[0];
                secilen_yon = 0;
                tap_menu_image.SetActive(false);
            }
        }
    }
}
