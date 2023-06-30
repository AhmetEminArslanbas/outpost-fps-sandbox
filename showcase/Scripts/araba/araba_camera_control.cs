using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class araba_camera_control : MonoBehaviour
{
    public GameObject hedef_araba;
    public Camera _camera;

    public Vector3 takip_uzakligi;
    public Quaternion takip_acisi;

    public float takip_smootlugu;

    private void Awake()
    {
        //_camera = Camera.main;
    }

    private void LateUpdate()
    {
        takip_acisi = transform.rotation;
        transform.LookAt(hedef_araba.transform);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, hedef_araba.transform.position + takip_uzakligi, takip_smootlugu * Time.deltaTime);
        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, transform.rotation, takip_smootlugu * Time.deltaTime);
    }

}
