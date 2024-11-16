using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    
    Camera mainCam;
    public float length=10f;
    
    void Start()
    {
        mainCam=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // ShootRay();
        
    }
    // Vector3 ShootRay (){
    //     Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
    //     // Ray ray = mainCam.ScreenPointToRay(screenCenter);

    //     Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane + length));

    //     Ray ray = mainCam.ScreenPointToRay(screenCenter);
    //     Vector3 rayEndpoint = ray.origin + ray.direction * length;
    //     Debug.DrawRay(ray.origin, ray.direction * length, Color.red); 

    //     return rayEndpoint;



    //     // Debug.DrawLine(mouseWorldPos,this.transform.position); // 10 units long, red color, lasts for 2 seconds
    //     // Debug.Log("RayCasted");

    // }
}
