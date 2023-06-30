using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_control : MonoBehaviour
{
    public GameObject player;
    public GameObject omuz_kol;

    public bool mouse_hareket_edebilir = true;

    public float hassasiyet;

    public float mouse_x;
    public float mouse_y;

    void FixedUpdate()
    {
        if(mouse_hareket_edebilir == true)
        {
            mouse_x += Input.GetAxis("Mouse X") * hassasiyet * Time.deltaTime;
            mouse_y += Input.GetAxis("Mouse Y") * hassasiyet * Time.deltaTime;
            mouse_y = Mathf.Clamp(mouse_y, -40, 40);

            player.transform.eulerAngles = new Vector3(transform.rotation.x, mouse_x, transform.rotation.z);
            gameObject.transform.eulerAngles = new Vector3(mouse_y, mouse_x, transform.rotation.z);
        }

        //omuz_kol.transform.eulerAngles = new Vector3(this.transform.localEulerAngles.x + 10, transform.eulerAngles.y, transform.eulerAngles.z); // çalışmıyor
    }
}
