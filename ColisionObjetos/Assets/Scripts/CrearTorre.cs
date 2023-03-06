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
        PiezaHier = new GameObject();
        PiezaHier.name = "Torre";

        construirTorre();



    }


    public void construirTorre()
    {
        Vector3 posInicial = new Vector3(-3.9f, 0.68f, 5.8f);
        //Vector3 cambioPosX = new Vector3(-1.19f, 0.0f, 0f);
        float cX = 0f; //cambio en x
        float cZ = 0f;
        float cY = 0f;
        
        for (int k = 0; k < 5; k++)
        {
            cZ = 0f;
            for (int j = 0; j < 5; j++)
            {
                cX = 0;
                for (int i = 0; i < 5; i++)
                {
                    GameObject piezaIns = Instantiate(piezaTorre, new Vector3(-2.5f + cX, 0.68f + cY, 5.8f + cZ), Quaternion.Euler(0, 0, 0));
                    piezaIns.name = "ParteTorreX" + (i + 1) + "Y" + (k + 1) + "Z" + (j + 1);
                    piezaIns.transform.parent = PiezaHier.transform;
                    cX += 1f;
                }
                cZ += 1f;
            }
            cY += 1.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
