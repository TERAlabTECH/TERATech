using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sphere : MonoBehaviour
{
    // Start is called before the first frame update
    float alfaValue;
    float duration =0.2f;

    Renderer sphereRenderer;
    public UnityEvent onTriggerSphere; 
    void Start()
    {
        sphereRenderer= GetComponent<Renderer>();
        onTriggerSphere.AddListener(()=>StartCoroutine(changeAlpha()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void triggerSphere(){
        onTriggerSphere.Invoke();
    }   
    
         
    IEnumerator changeAlpha(){
        
        // Gradually increase alpha from 0 to 1 over 'duration' seconds
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            alfaValue = Mathf.Lerp(0, 1, elapsedTime / duration);
            sphereRenderer.material.SetFloat("_Alfa", alfaValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Gradually decrease alpha from 1 to 0 over 'duration' seconds
        elapsedTime = 0f;
        while (elapsedTime <= duration ||  alfaValue>0)
        {
            alfaValue = Mathf.Lerp(1, 0f, elapsedTime / duration);
            sphereRenderer.material.SetFloat("_Alfa", alfaValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
    }

}
