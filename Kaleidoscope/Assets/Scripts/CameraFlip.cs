using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{

    [SerializeField] private bool flip = false;
    [Range(-1,1)]
    [SerializeField] private float xFlip = 1; 
    [Range(-1,1)]
    [SerializeField] private float yFlip = 1; 

    private Camera cameraFlip;
    private Matrix4x4 originalProjectionMatrix;

    // Start is called before the first frame update
    void Start()
    {
        cameraFlip = GetComponent<Camera>();
        originalProjectionMatrix = cameraFlip.projectionMatrix;
    }

    // Update is called once per frame
    void Update()
    {
        if(flip){
            cameraFlip.projectionMatrix = originalProjectionMatrix * Matrix4x4.Scale(new Vector3(xFlip,yFlip,1));
            flip = false;
        }
    }
}
