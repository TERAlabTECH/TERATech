using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

public class Movement : MonoBehaviour
{
    public GameObject fireTrail;
    public XROrigin arSessionOrigin;
    Vector3 direction;

    [SerializeField] float moveSpeed = 5f;
    public float rotationSpeed = 1f;

    private float angleToRotateOnZ = 0;
    private float angleToRotateOnX = 0;

    void Start()
    {
        if (arSessionOrigin == null)
        {
            Debug.LogError("AR Session Origin is not assigned. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        if (arSessionOrigin == null)
            return;

        // Obtener la rotación de la cámara
        Quaternion deviceRotation = arSessionOrigin.Camera.transform.rotation;

        // Ajustar la rotación del dispositivo para que sea coherente con el espacio del juego
        deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);

        // Calcular la dirección en un espacio 2D basado en la rotación del dispositivo
        direction = new Vector3(deviceRotation.x, deviceRotation.y, 0).normalized;

        // Mover la nave espacial en la dirección calculada sin cambiar la distancia hacia la cámara
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Interpolar suavemente a la rotación objetivo
        InterpolateRotationOnXY(deviceRotation);
    }

    void InterpolateRotationOnXY(Quaternion deviceRotation)
    {
        // Calcular los ángulos objetivo en función de la rotación del dispositivo
        angleToRotateOnZ = Mathf.InverseLerp(-2.7f, 2.7f, deviceRotation.x) - 0.5f;
        angleToRotateOnZ *= 120;

        angleToRotateOnX = Mathf.InverseLerp(-0.4f, 0.4f, deviceRotation.y) - 0.5f;
        angleToRotateOnX *= -60;

        // Log de rotación objetivo
        Debug.Log($"Rotating x by: {angleToRotateOnX}, z by: {angleToRotateOnZ}");

        // Crear la rotación objetivo basada en los ángulos calculados
        Quaternion targetRotation = Quaternion.Euler(angleToRotateOnX, 0, angleToRotateOnZ);

        // Interpolar suavemente a la rotación objetivo
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}