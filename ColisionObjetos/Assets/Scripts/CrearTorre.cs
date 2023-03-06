using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearTorre : MonoBehaviour
{
    [SerializeField] private GameObject piezaTorre;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(piezaTorre, new Vector3(0, 0.53f, 0), Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
