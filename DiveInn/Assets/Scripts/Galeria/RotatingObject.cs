using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed=10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationAmount, 0f);
    }
}
