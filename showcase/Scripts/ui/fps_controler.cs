using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fps_controler : MonoBehaviour
{
    public Text fps_text;

    float fps;
    [SerializeField] float gosterme_araligi;

    private void Start()
    {
        InvokeRepeating(nameof(fps_ekrana_goster), 0, gosterme_araligi);
    }

    private void LateUpdate()
    {
        fps = 1 / Time.deltaTime;
    }

    void fps_ekrana_goster()
    {
        int int_fps = (int)fps;
        fps_text.text = "fps: " + int_fps.ToString();
    }
}
