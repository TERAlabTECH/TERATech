using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject fireTrail;
    Vector3 mouseWorldPos; 
    Vector3 playerPos;
    Vector3 direction;
    [SerializeField] float moveSpeed= 5f;

    [SerializeField] Camera mainCam;
    void Start(){
        mainCam=Camera.main;
    }
    void Update()
    {
        // Obtener la posición del mouse en el mundo
        mouseWorldPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane + 2));
        playerPos = transform.position;

        if (Vector3.Distance(mouseWorldPos, playerPos) > 0.01f)
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
    }

    
}
