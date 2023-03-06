using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearTorre : MonoBehaviour
{
    [SerializeField] private GameObject piezaTorre;
    private GameObject PiezaHier; //Hier de jerarquía

    // Start is called before the first frame update
    void Start()
    {
        Vector3 posInicial = new Vector3(-0.43f, 0.53f, 5.8f);
        //Vector3 cambioPosX = new Vector3(-1.19f, 0.0f, 0f);
        float cX = 0f; //cambio en x

        PiezaHier = new GameObject();
        PiezaHier.name = "Torre"; 

        for(int i=0; i<5; i++)
        {
            GameObject piezaIns = Instantiate(piezaTorre, new Vector3(0.3f + cX, 0.53f, 5.8f), Quaternion.Euler(0, 0, 0));
            piezaIns.name = "ParteTorre"+(i+1);
            piezaIns.transform.parent = PiezaHier.transform;
            cX += 1.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
