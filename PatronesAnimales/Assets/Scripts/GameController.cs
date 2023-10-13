using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject personaje;
    [SerializeField] private GameObject suelo;
    private Personaje p;
    private Vector3 originalPos;
    private Vector3 posSuelo;
    private Vector3 posPersonaje;
    private float radioCirculoZ, radioCirculoX;


    private void Start()
    { 
        p = personaje.gameObject.GetComponent<Personaje>();
        originalPos = new Vector3(p.gameObject.transform.position.x, p.gameObject.transform.position.y, p.gameObject.transform.position.z);
        Debug.Log(originalPos);
        Debug.Log("Suelo" + posSuelo);
    }

    //void Update()
    //{
    //    Debug.Log(p.gameObject.transform.position);

    //    if (p.transform.position.z >= -0.216f && p.transform.position.z < 3.27f)
    //    {
    //        p.transform.Translate(0, 0, Time.deltaTime * 0.05f); //deltaTime es el tiempo entre un frame y otro
    //    } else if (p.transform.position.z >= 3.27f) {
    //        p.transform.Translate(0, 0, Time.deltaTime * -0.05f);
    //    }
    //}

    float timeCounter = 0;

    void Update()
    {
        radioCirculoZ = suelo.gameObject.transform.position.z / 2;
        timeCounter += Time.deltaTime;
        float x = suelo.gameObject.transform.position.x + radioCirculoZ * Mathf.Cos(timeCounter);
        float z = suelo.gameObject.transform.position.z + radioCirculoZ * Mathf.Sin(timeCounter);
        Debug.Log(originalPos);
        p.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
    }

}
