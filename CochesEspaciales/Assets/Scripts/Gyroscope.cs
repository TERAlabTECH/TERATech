using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    Camera mainCam;
    Quaternion rotationFix;
    private Quaternion initialGyroRotation; // Calibration offset

    void Start()
    {
        if(SystemInfo.supportsGyroscope){
            Input.gyro.enabled = true;
        }
        rotationFix= new Quaternion(0,0,1,0);
        initialGyroRotation = Input.gyro.attitude;
        mainCam = Camera.main;
    }


    void Update()
    {
        if(SystemInfo.supportsGyroscope){
            transform.rotation= Quaternion.Euler(90, 0, 0)*Input.gyro.attitude * rotationFix; 
            //transform.rotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, Input.gyro.attitude.z, Input.gyro.attitude.w) * Quaternion.Euler(90f, 0f, 0f) * rotationFix;
        }
    }

}
