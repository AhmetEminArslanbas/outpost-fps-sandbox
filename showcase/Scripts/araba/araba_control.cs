using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class araba_control : MonoBehaviour
{
    public GameObject onsag;
    public GameObject onsol;
    public GameObject arkasag;
    public GameObject arkasol;
    
    public WheelCollider onsag_col;
    public WheelCollider onsol_col;
    public WheelCollider arkasag_col;
    public WheelCollider arkasol_col;

    public static bool araba_hareket;

    public float max_motor_gucu;
    public float max_donus_aci;
    public float motor_torku;
    public float donus;

    public float fren_gucu;
    public float fren;

    private void FixedUpdate()
    {
        if(araba_hareket)
        {
            motor_torku = max_motor_gucu * Input.GetAxis("Vertical");
            donus = max_donus_aci * Input.GetAxis("Horizontal");
            fren = fren_gucu * Input.GetAxis("Jump");

            onsol_col.steerAngle = onsag_col.steerAngle = donus;

            arkasag_col.motorTorque = motor_torku;
            arkasol_col.motorTorque = motor_torku;

            if (Input.GetKey(KeyCode.Space))
            {
                arkasag_col.brakeTorque = fren_gucu;
                arkasag_col.brakeTorque = fren_gucu;
            }
            else
            {
                arkasag_col.brakeTorque = 0;
                arkasag_col.brakeTorque = 0;
            }
            /*
            if(motor_torku == 0)
            {
                arkasag_col.brakeTorque = fren_gucu;
                arkasol_col.brakeTorque = fren_gucu;
            }*/
        }

        teker_hareket_goruntu();
    }

    void teker_hareket_goruntu()
    {
        Vector3 pos;
        Quaternion rot;


        onsag_col.GetWorldPose(out pos, out rot);
        onsag.transform.position = pos;
        onsag.transform.rotation = rot;

        onsol_col.GetWorldPose(out pos, out rot);
        onsol.transform.position = pos;
        onsol.transform.rotation = rot;

        arkasag_col.GetWorldPose(out pos, out rot);
        arkasag.transform.position = pos;
        arkasag.transform.rotation = rot;

        arkasol_col.GetWorldPose(out pos, out rot);
        arkasol.transform.position = pos;
        arkasol.transform.rotation = rot;


    }

}
