using UnityEngine;

public class SpaceshipGyroController : MonoBehaviour
{
    public Camera mainCam; // Asigna la cámara principal
    public GameObject spaceship; // Asigna la nave en el inspector
    public float moveSpeed = 5f; // Velocidad de movimiento de la nave
    public float offsetDistance = 2f; // Distancia entre la cámara y la nave
    public float rotationSpeed = 5f; // Velocidad de rotación de la nave

    private Quaternion rotationFix; // Ajuste para alinear giroscopio y Unity
    private Quaternion initialRotation; // Orientación inicial del giroscopio
    private Vector3 targetPosition;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

        // Corrección del sistema de coordenadas
        rotationFix = new Quaternion(0, 0, 1, 0);

        // Guarda la cámara principal
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        // Establecer la orientación inicial
        initialRotation = Input.gyro.attitude;
    }

    void Update()
    {
        if (!SystemInfo.supportsGyroscope) return;

        // Obtener la rotación del giroscopio
        Quaternion gyroRotation = Input.gyro.attitude * rotationFix;

        // Invertir los ejes necesarios
        //gyroRotation.x *= -1; // Corregir inversión del eje X
        //gyroRotation.y *= -1; // Corregir inversión del eje Y

        // Ajustar con la orientación inicial
        gyroRotation = Quaternion.Inverse(initialRotation) * gyroRotation;

        // Rotar la cámara
        mainCam.transform.rotation = Quaternion.Euler(90, 0, 0) * gyroRotation; //lo multiplicamos por 90 en x p

        // Calcular la posición deseada de la nave con un offset
        targetPosition = mainCam.transform.position + mainCam.transform.forward * offsetDistance;

        // Interpolar la posición de la nave hacia el objetivo
        spaceship.transform.position = Vector3.Lerp(spaceship.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotar la nave suavemente hacia la posición de destino
        RotateSpaceshipTowardsTarget(targetPosition);
    }

    void RotateSpaceshipTowardsTarget(Vector3 target)
    {
        // Calcular la dirección hacia el objetivo
        Vector3 direction = (target - spaceship.transform.position).normalized;

        // Calcular la rotación objetivo basada en la dirección
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Suavizar la rotación de la nave
        spaceship.transform.rotation = Quaternion.Slerp(spaceship.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}