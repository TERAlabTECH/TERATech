
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 0.00001f;
    public bool flipY=false; 
    public bool rotateX=false;

    public GameObject lvlManagerObject;
    LevelManager lvlManagerScript;
    public Vector3 initialPos; 

    void Start(){
        lvlManagerScript = lvlManagerObject.GetComponent<LevelManager>();
        transform.position=initialPos;
    }

    void Update()
    {

        if(!lvlManagerScript.paused){
            // Get input from the WASD keys
            float moveX = Input.GetAxisRaw("Horizontal"); // A/D keys for left/right movement
            float moveY = Input.GetAxisRaw("Vertical");   // W/S keys for up/down movement       

            //  up/down arrow avanzar atrazarse
            Vector2 movement = new Vector2(0, moveY).normalized;
            transform.Translate(movement * moveSpeed * Time.deltaTime*0.1f);

            //left/right arrow cmabian el angulo
            Vector3 rotation = new Vector3(0,0,-moveX).normalized;
            transform.Rotate(rotation*100f *Time.deltaTime);
        }
    }

    public void SetToInitalPosition(){
        transform.position=initialPos;
    }
    
}

