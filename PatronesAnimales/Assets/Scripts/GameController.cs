using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


// Como un futuro paso podemos:
//      - hacer que se randomice también la posición inicial de cada personaje
//      - en vez de que se tengan que inicializar cada personaje individualmente, podríamos generarlos dinámicamente en este script

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

    private float[] pT1Radio;
    private float[] pT1Speed;


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

        pT1Radio = new float[3];
        for (int i = 0; i < 3; i++)
        {
            pT1Radio[i] = restrictCircularMovement(pT1initialOffset[i]);
        }

        pT1Speed = new float[] {
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
        };
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
            radioCirculo = pT1Radio[i];
            x = suelo.gameObject.transform.position.x + pT1initialOffset[i].x - (float)Math.Pow(-1, i) * (radioCirculo * Mathf.Cos(timeCounter * pT1Speed[i]));
            z = suelo.gameObject.transform.position.z + pT1initialOffset[i].z - (float)Math.Pow(-1, i) * (radioCirculo * Mathf.Sin(timeCounter * pT1Speed[i]));

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="initialOffset"></param>
    /// <returns></returns>
    public float restrictCircularMovement(Vector3 initialOffset) {
        float res;
        float quadrantSize = suelo.gameObject.transform.localScale.z / 2;//Supone que el suelo es un cuadrado no rectangulo
        float x = Math.Abs(initialOffset.x);
        float z = Math.Abs(initialOffset.z);

        float distanceToEdgeX = quadrantSize - x;
        float distanceToEdgeZ = quadrantSize - z;

        res = Mathf.Min(distanceToEdgeX, distanceToEdgeZ);

        return UnityEngine.Random.Range(0.05f, res); 
    }

    public float differentSpeeds() {
        float res = 0.0f;

        return res;
    }

}
