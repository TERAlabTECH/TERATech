using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GyroMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject fireTrail;
    Vector3 rayEndpoint; 
    Vector3 playerPos;
    Vector3 direction;
    [SerializeField] float moveSpeed= 5f;

    [SerializeField] Camera mainCam;
    public float rotationSpeed=1;
    private float distToRay;
    private float distToX;
    private float distToY;

    private float angleToRotateOnZ=0;
    private float angleToRotateOnX=0;
    void Start(){
        mainCam=Camera.main;
    }
    void Update()
    {
        // Rotate constantly on the z-axis
        // transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Obtener la posición del mouse en el mundo
        rayEndpoint = ShootRay();
        playerPos = transform.position;
        distToRay = Vector3.Distance(rayEndpoint, playerPos);
       

        InterpolateRotationOnXY();


        if(distToRay > 0.01f)
        {

            // fireTrail.SetActive(true);
            direction = (rayEndpoint - playerPos).normalized;
            
            // Interpolacion hacia la posición del mouse 
            transform.position = Vector3.Lerp(transform.position, rayEndpoint, moveSpeed * Time.deltaTime);
        }
        
       
    }
    

    void InterpolateRotationOnXY()
    {
        // First, calculate the target angles based on the distances
        distToX = rayEndpoint.x - playerPos.x;
        angleToRotateOnZ = Mathf.InverseLerp(-2.7f, 2.7f, distToX) - 0.5f;
        angleToRotateOnZ *= 120;

        distToY = rayEndpoint.y - playerPos.y;
        angleToRotateOnX = Mathf.InverseLerp(-.4f, .4f, distToY) - 0.5f;
        angleToRotateOnX *= -60;
        // Debug.Log($"Rotating x by: {angleToRotateOnX}" );

        // Create the target rotation based on the calculated angles
        Quaternion targetRotation = Quaternion.Euler(angleToRotateOnX, 180, angleToRotateOnZ);

        // Interpolate smoothly to the target rotation using Quaternion.Lerp or Quaternion.Slerp
        float rotationSpeed = 5f; // Adjust this speed as needed for smoothness
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    [SerializeField]float distToShip;
    Vector3 ShootRay(){
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);


        Ray ray = mainCam.ScreenPointToRay(screenCenter);
        Vector3 rayEndpoint = ray.origin + ray.direction * distToShip;
        Debug.DrawRay(ray.origin, ray.direction * distToShip, Color.red); 
        Debug.Log("ShotRay");

        return rayEndpoint;
    }


    
}
