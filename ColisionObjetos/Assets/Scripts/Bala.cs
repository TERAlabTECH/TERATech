using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject pistola;
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
        }
        
    }

    public void agregarBala()
    {
        if (listaBalas.Count == 0)
        {
            Debug.Log(pistola.transform.position);
            GameObject balaIns = Instantiate(bala, pistola.transform.position - new Vector3(0f, 1.8f, 0f), Quaternion.Euler(0, 0, 0));
            listaBalas.Add(balaIns);
        }
        else
        {

        }
    }
}
