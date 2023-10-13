using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [Header("Personaje Tipo 1")] //Los diferentes objetos que están en la escena que quiero asociar para darles funcionamiento/obtener info
    [SerializeField] private GameObject personaje1Tipo1;
    [SerializeField] private GameObject personaje2Tipo1;
    [SerializeField] private GameObject personaje3Tipo1;

    [SerializeField] private GameObject personajeTipo2;
    [SerializeField] private GameObject personajeTipo3;
    [SerializeField] private GameObject suelo;

    //Variables internas a la clase
    private Personaje[] pT1;
    private Vector3[] pT1initialOffset;

    private Personaje p1T1;
    private Personaje p2T1;
    private Personaje p3T1;

    private Personaje p1T2;
    private Personaje p2T2;
    private Personaje p3T2;

    private Personaje p1T3;
    private Personaje p2T3;
    private Personaje p3T3;

    float timeCounter = 0;

    private void Start()
    {
        p1T1 = personaje1Tipo1.gameObject.GetComponent<Personaje>();
        p2T1 = personaje2Tipo1.gameObject.GetComponent<Personaje>();
        p3T1 = personaje3Tipo1.gameObject.GetComponent<Personaje>();

        pT1 = new Personaje[] { p1T1,p2T1,p3T1};
        pT1initialOffset = new Vector3[] {
            p1T1.gameObject.transform.position - suelo.transform.position,
            p2T1.gameObject.transform.position - suelo.transform.position,
            p3T1.gameObject.transform.position - suelo.transform.position
        }; 


        //originalPos = new Vector3(p1T1.gameObject.transform.position.x, p1T1.gameObject.transform.position.y, p1T1.gameObject.transform.position.z);
        //Debug.Log(originalPos);
        //Debug.Log("Suelo" + posSuelo);
    }

    //Se llama en cada frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        moverPersonajeTipo1();

    }

    //Mueve los objetos en una trayectoria circular
    public void moverPersonajeTipo1() {
        float x, z;
        float radioCirculo;
        for (int i = 0; i < 3; i++) {
            radioCirculo = restrictCircularMovement(pT1initialOffset[i]);
            x = suelo.gameObject.transform.position.x + pT1initialOffset[i].x - radioCirculo * Mathf.Cos(timeCounter);
            z = suelo.gameObject.transform.position.z + pT1initialOffset[i].z - radioCirculo * Mathf.Sin(timeCounter);

            switch (i) {
                case 0:
                    p1T1.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 1:
                    p2T1.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 2:
                    p3T1.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
            }
        }
    }

    public float restrictCircularMovement(Vector3 initialOffset) {
        float res;
        float quadrantSize = suelo.gameObject.transform.localScale.z / 2;//Supone que el suelo es un cuadrado no rectangulo
        float x = Math.Abs(initialOffset.x);
        float z = Math.Abs(initialOffset.z);

        float distanceToEdgeX = quadrantSize - x;
        float distanceToEdgeZ = quadrantSize - z;

        res = Mathf.Min(distanceToEdgeX, distanceToEdgeZ);

        return res;
    }
}
