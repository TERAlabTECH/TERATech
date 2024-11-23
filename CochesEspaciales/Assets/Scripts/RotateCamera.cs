using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 10f; // Velocidad de rotación

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); // Rotar alrededor del eje Y
    }
}