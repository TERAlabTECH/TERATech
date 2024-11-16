using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    Camera mainCam;
    Quaternion rotationFix;
    void Start()
    {
        if(SystemInfo.supportsGyroscope){
            Input.gyro.enabled = true;
        }
        rotationFix= new Quaternion(0,0,1,0);
        mainCam=Camera.main;
    }

    
    void Update()
    {
        if(SystemInfo.supportsGyroscope){
            transform.rotation=Input.gyro.attitude*rotationFix*Quaternion.Euler(90,0,0); 
        }
        
    }

}
