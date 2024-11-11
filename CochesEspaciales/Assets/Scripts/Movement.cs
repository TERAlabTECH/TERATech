using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireTrail;
    Vector3 mouseWorldPos;
    Vector3 playerPos;
    Vector3 direction;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Camera mainCam;
    public float rotationSpeed = 1;
    private float distToMouse;
    private float distToX;
    private float distToY;

    private float angleToRotateOnZ = 0;
    private float angleToRotateOnX = 0;
    void Start()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
        // Rotate constantly on the z-axis
        // transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Obtener la posición del mouse en el mundo
        mouseWorldPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane + 3));
        playerPos = transform.position;
        distToMouse = Vector3.Distance(mouseWorldPos, playerPos);


        InterpolateRotationOnXY();


        if (distToMouse > 0.01f)
        {

            // fireTrail.SetActive(true);
            direction = (mouseWorldPos - playerPos).normalized;

            // Interpolacion hacia la posición del mouse 
            transform.position = Vector3.Lerp(transform.position, mouseWorldPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            // fireTrail.SetActive(false);
        }
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     Debug.Log($"{}");
        // }


    }


    void InterpolateRotationOnXY()
    {
        // First, calculate the target angles based on the distances
        distToX = mouseWorldPos.x - playerPos.x;
        angleToRotateOnZ = Mathf.InverseLerp(-2.7f, 2.7f, distToX) - 0.5f;
        angleToRotateOnZ *= 120;

        distToY = mouseWorldPos.y - playerPos.y;
        angleToRotateOnX = Mathf.InverseLerp(-.4f, .4f, distToY) - 0.5f;
        angleToRotateOnX *= -60;
        Debug.Log($"Rotating x by: {angleToRotateOnX}");

        // Create the target rotation based on the calculated angles
        Quaternion targetRotation = Quaternion.Euler(angleToRotateOnX, 180, angleToRotateOnZ);

        // Interpolate smoothly to the target rotation using Quaternion.Lerp or Quaternion.Slerp
        float rotationSpeed = 5f; // Adjust this speed as needed for smoothness
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }



}