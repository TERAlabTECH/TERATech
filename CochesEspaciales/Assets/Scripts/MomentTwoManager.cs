using UnityEngine;

public class MovementTwoManager : MonoBehaviour
{
    // Reference objects
    public Transform referenceObject;
    public Transform spaceship;

    // Movement settings
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 0.1f;
    public float referenceObjectOffset = 5f;

    private Camera mainCam;

    void Start()
    {
        // Habilitar el giroscopio
        Input.gyro.enabled = true;

        // Obtener la cámara principal
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        // Verificar si el giroscopio está disponible
        if (!Input.gyro.enabled)
        {
            Debug.LogWarning("El giroscopio no está habilitado.");
            return;
        }

        // Obtener la rotación del giroscopio
        Quaternion gyroRotation = Input.gyro.attitude;

        // Ajustar la orientación para que coincida con el sistema de coordenadas de Unity
        gyroRotation = new Quaternion(gyroRotation.x, gyroRotation.y, -gyroRotation.z, -gyroRotation.w);

        // Actualizar la rotación de la cámara con el giroscopio
        mainCam.transform.rotation = gyroRotation;

        // Calcular dirección hacia adelante desde el giroscopio
        Vector3 forward = gyroRotation * Vector3.forward;

        // Actualizar la posición del ReferenceObject
        Vector3 targetPosition = mainCam.transform.position + forward * referenceObjectOffset;
        referenceObject.position = Vector3.Lerp(referenceObject.position, targetPosition, moveSpeed * Time.deltaTime);

        // Calcular la distancia al ReferenceObject
        float distanceToReference = Vector3.Distance(spaceship.position, referenceObject.position);

        // Actualizar la posición y rotación de la nave si está fuera de la distancia de parada
        if (distanceToReference > stoppingDistance)
        {
            Vector3 direction = (referenceObject.position - spaceship.position).normalized;
            spaceship.position = Vector3.Lerp(spaceship.position, referenceObject.position, moveSpeed * Time.deltaTime);

            // Interpolar la rotación para simular el efecto de inclinación
            InterpolateRotationOnXY(referenceObject.position);
        }
        else
        {
            // Estabilizar la rotación de la nave cuando esté cerca
            Quaternion stabilizeRotation = Quaternion.Euler(0, 180, 0);
            spaceship.rotation = Quaternion.Slerp(spaceship.rotation, stabilizeRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void InterpolateRotationOnXY(Vector3 targetPosition)
    {
        // Calcular ángulo de inclinación basado en la distancia X e Y al ReferenceObject
        Vector3 playerPos = spaceship.position;
        float distToX = targetPosition.x - playerPos.x;
        float distToY = targetPosition.y - playerPos.y;

        // Aplicar InverseLerp para suavizar el efecto de inclinación
        float angleToRotateOnZ = Mathf.InverseLerp(-3f, 3f, distToX) - 0.5f;
        angleToRotateOnZ *= 90; // Ajustar fuerza de inclinación en el eje Z

        float angleToRotateOnX = Mathf.InverseLerp(-0.5f, 0.5f, distToY) - 0.5f;
        angleToRotateOnX *= -45; // Ajustar fuerza de inclinación en el eje X

        // Crear rotación objetivo con los ángulos calculados
        Quaternion targetRotation = Quaternion.Euler(angleToRotateOnX, 180, angleToRotateOnZ);

        // Interpolar suavemente hacia la rotación objetivo
        spaceship.rotation = Quaternion.Slerp(spaceship.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
