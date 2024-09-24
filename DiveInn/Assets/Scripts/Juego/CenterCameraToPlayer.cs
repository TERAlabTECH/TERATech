using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCameraToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform diver; 
    public Vector3 initialPos;
    void Start()
    {
        initialPos=transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position=new Vector3(diver.position.x, diver.position.y, initialPos.z);
    }
}
