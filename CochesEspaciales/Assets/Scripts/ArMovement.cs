using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArMovement : MonoBehaviour
{
    public GameObject fireTrail;
    public GameObject spaceship;
    public ARSessionOrigin arSessionOrigin;

    Vector3 direction;
    [SerializeField] float moveSpeed = 5f;
    private float rotationSpeed = 5f;
    private float angleToRotateOnZ = 0;
    private float angleToRotateOnX = 0;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        // Obtener la rotación del dispositivo
        Quaternion deviceRotation = Input.gyro.attitude;

        // Ajustar la rotación del dispositivo
        deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);

        // Aplicar la rotación al origen de la sesión
        arSessionOrigin.transform.localRotation = deviceRotation;

        InterpolateRotationOnXY();

        // Obtener la dirección del movimiento solo en X e Y
        direction = arSessionOrigin.transform.right * deviceRotation.x + arSessionOrigin.transform.up * deviceRotation.y;
        direction.z = 0; // Asegúrate de que la dirección en el eje Z sea cero

        // Mover la nave espacial con la rotación del dispositivo en X e Y solamente
        spaceship.transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void InterpolateRotationOnXY()
    {
        // Obtener las distancias basadas en la rotación del dispositivo
        float distToX = Input.gyro.attitude.x;
        float distToY = Input.gyro.attitude.y;

        // Calcular los ángulos de rotación
        angleToRotateOnZ = Mathf.InverseLerp(-2.7f, 2.7f, distToX) - 0.5f;
        angleToRotateOnZ *= 120;

        angleToRotateOnX = Mathf.InverseLerp(-0.4f, 0.4f, distToY) - 0.5f;
        angleToRotateOnX *= -60;

        // Crear la rotación objetivo basada en los ángulos calculados
        Quaternion targetRotation = Quaternion.Euler(angleToRotateOnX, 180, angleToRotateOnZ);

        // Interpolar suavemente a la rotación objetivo
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}