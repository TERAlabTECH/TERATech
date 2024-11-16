using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    void Start()
    {
        if(SystemInfo.supportsGyroscope){
            Input.gyro.enabled = true;
        }
    }

    
    void Update()
    {
        transform.rotation=Input.gyro.attitude; 
    }
}
