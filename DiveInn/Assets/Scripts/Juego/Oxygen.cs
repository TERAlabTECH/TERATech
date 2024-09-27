using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelManager;
    public LevelManager lvlManager;
    [Range(1,240)]public float tiempoDeOxigeno=120f;

    Image imagen;
    Material oxygenBar;
    float percentage=100;


    void Start()
    {
        lvlManager=levelManager.GetComponent<LevelManager>();
        imagen= GetComponent<Image>();
        percentage=100;
        oxygenBar= imagen.materialForRendering;
    }

    // Update is called once per frame
    void Update()
    {
        if(!lvlManager.paused){
            percentage -= (100f / tiempoDeOxigeno) * Time.deltaTime;
            Debug.Log(percentage);
            oxygenBar.SetFloat("_AlphaCutoff", percentage);
        }
    }
    void OnApplicationQuit(){
        percentage=100;
    }
}
