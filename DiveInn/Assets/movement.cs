using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Transform trans; 

    // Start is called before the first frame update
    void Start()
    {
        trans= gameObject.GetComponent<Transform>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        trans.position+=new Vector3(-0.01f,0,0);
    }
}
