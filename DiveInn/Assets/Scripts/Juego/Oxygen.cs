using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Oxygen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelManager;
    public LevelManager lvlManager;
    float tiempoDeOxigeno;

    Image imagen;
    Material oxygenBar;
    float percentage=100;


    void Start()
    {
        lvlManager=levelManager.GetComponent<LevelManager>();
        imagen= GetComponent<Image>();

        tiempoDeOxigeno=lvlManager.tiempoInicialDeOxigeno;

        percentage=100;
        oxygenBar= imagen.materialForRendering;

    }

    // Update is called once per frame
    void Update()
    {
        if(!lvlManager.paused){
            percentage -= (100f / tiempoDeOxigeno) * Time.deltaTime;
            oxygenBar.SetFloat("_AlphaCutoff", percentage);
            lvlManager.porcentajeDeOxigeno=percentage;
        }
    }
    void OnApplicationQuit(){
        percentage=100;
    }

    void OnEnable(){
        LevelManager.OnRestart+=Start;
    }
    void OnDisable(){
        LevelManager.OnRestart-=Start;
    }
}
