using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] List<GameObject> listaBalas = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            agregarBala();
            //if (listaBalas.Count == 0)
            //{
            //    GameObject balaIns = Instantiate(bala, new Vector3(2.8f, 2.5f, -3.2f), Quaternion.Euler(0, 0, 0));
            //}
            //else
            //{

            //}
        }
        
    }

    public void agregarBala()
    {
        if (listaBalas.Count == 0)
        {
            GameObject balaIns = Instantiate(bala, new Vector3(2.8f, 2.5f, -3.2f), Quaternion.Euler(0, 0, 0));
            listaBalas.Add(balaIns);
        }
        else
        {

        }
    }
}
