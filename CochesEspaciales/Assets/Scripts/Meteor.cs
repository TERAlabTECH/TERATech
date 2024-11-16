using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour

{
    public GameObject playerShip;
    public Vector3 direction; 
    public Vector3 origin; 
    public Vector3 destination; 
    private float timePassed; 
    private float brightnessMultiplier;
    private Vector3 screenCenter; 

    public float speed;
    // Start is called before the first frame update
    public void ControlledStart()
    {
        screenCenter= new Vector3(-3, -89, -469);

        origin= transform.position; 
        
        ClosenessToCenter();

        SetDirectionVector(); 
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
       
            timePassed+=Time.deltaTime*0.4f;
            //posision actual del meteorito
            GetPosition(timePassed);
            SetBrightness();
            // Debug.DrawLine(transform.position, playerShip.transform.position);

    }
    void SetDirectionVector(){
        direction= destination-origin;
    }

    
    void GetPosition(float t){
        float x= (1-t)*Mathf.Sin(t*Mathf.PI*10);
        float y= (1-t)*Mathf.Sin(-t*Mathf.PI*10);
        float z= t;

        // Vector3 newDirection= new Vector3(direction.x*x,direction.y*y,direction.z*t); 
        Vector3 newDirection= new Vector3(direction.x*t,direction.y*t,direction.z*t); 


        transform.position= origin+newDirection;
    }   
    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }  

    void SetBrightness(){
        brightnessMultiplier=timePassed; 
        // Debug.Log($"BrightnessMultiplier: {brightnessMultiplier}");
        GetComponent<MeshRenderer>().materials[1].SetFloat("_Alfa", 0.19f*brightnessMultiplier);
    } 

    float ClosenessToCenter(){
        float dist= (Vector3.Distance(destination,screenCenter));
        return dist;
    }
    
}
