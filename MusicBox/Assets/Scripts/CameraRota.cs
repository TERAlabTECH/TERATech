using UnityEngine;
using System.Collections;

public class AutoRotateCamera : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    void Start()
    {
        StartCoroutine(RotateCamera());
    }

    IEnumerator RotateCamera()
    {
        while (true)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}