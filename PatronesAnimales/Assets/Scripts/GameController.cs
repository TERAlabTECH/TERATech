using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject personaje;
    private Personaje p;
    private Vector3 originalPos;


    private void Start()
    { 
        p = personaje.gameObject.GetComponent<Personaje>();
        originalPos = new Vector3(p.gameObject.transform.position.x, p.gameObject.transform.position.y, p.gameObject.transform.position.z);
        Debug.Log(originalPos);
    }

    void Update()
    {
        Debug.Log(p.gameObject.transform.position);

        if (p.transform.position.z >= -0.216f && p.transform.position.z < 3.27f)
        {
            p.transform.Translate(0, 0, Time.deltaTime * 0.05f); //deltaTime es el tiempo entre un frame y otro
        } else if (p.transform.position.z >= 3.27f) {
            p.transform.Translate(0, 0, Time.deltaTime * -0.05f);
        }
    }

}
