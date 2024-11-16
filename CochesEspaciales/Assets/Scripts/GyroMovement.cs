using UnityEngine;

public class GyroMovement : MonoBehaviour
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
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        // Update ReferenceObject position
        Vector3 forward = mainCam.transform.forward;
        Vector3 targetPosition = mainCam.transform.position + forward * referenceObjectOffset;
        referenceObject.position = Vector3.Lerp(referenceObject.position, targetPosition, moveSpeed * Time.deltaTime);

        // Calculate distance to ReferenceObject
        float distanceToReference = Vector3.Distance(spaceship.position, referenceObject.position);

        // Update spaceship position and rotation if beyond stopping distance
        if (distanceToReference > stoppingDistance)
        {
            Vector3 direction = (referenceObject.position - spaceship.position).normalized;
            spaceship.position = Vector3.Lerp(spaceship.position, referenceObject.position, moveSpeed * Time.deltaTime);

            // Interpolate rotation to simulate tilt effect
            InterpolateRotationOnXY(referenceObject.position);
        }
        else
        {
            // Stabilize spaceship to face forward with no tilt when close enough
            Quaternion stabilizeRotation = Quaternion.Euler(0, 180, 0);
            spaceship.rotation = Quaternion.Slerp(spaceship.rotation, stabilizeRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void InterpolateRotationOnXY(Vector3 targetPosition)
    {
        // Calculate tilt angle based on the X and Y distance to the target position
        Vector3 playerPos = spaceship.position;
        float distToX = targetPosition.x - playerPos.x;
        float distToY = targetPosition.y - playerPos.y;

        // Apply InverseLerp for smoother and limited tilt effects
        float angleToRotateOnZ = Mathf.InverseLerp(-3f, 3f, distToX) - 0.5f;
        angleToRotateOnZ *= 90;  // Adjust Z-axis tilt strength

        float angleToRotateOnX = Mathf.InverseLerp(-0.5f, 0.5f, distToY) - 0.5f;
        angleToRotateOnX *= -45; // Adjust X-axis tilt strength

        // Create target rotation with calculated tilt angles
        Quaternion targetRotation = Quaternion.Euler(angleToRotateOnX, 180, angleToRotateOnZ);

        // Smoothly interpolate to target rotation
        spaceship.rotation = Quaternion.Slerp(spaceship.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}