using UnityEngine;
using System.Collections;

public class AutoRotateCameraXY : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public bool rotateX = true;
    public bool rotateY = true;

    void Start()
    {
        StartCoroutine(RotateCamera());
    }

    IEnumerator RotateCamera()
    {
        while (true)
        {
            if (rotateX)
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
            if (rotateY)
            {
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }
    }
}
