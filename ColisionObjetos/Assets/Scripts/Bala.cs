using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject pistola;
    [SerializeField] List<GameObject> listaBalas = new List<GameObject>();
    private GameObject BalasHier;

    // Start is called before the first frame update
    void Start()
    {
        BalasHier = new GameObject();
        BalasHier.name = "Balas";
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
            GameObject balaIns = Instantiate(bala, pistola.transform.position - new Vector3(0f, 1.8f, 0f), Quaternion.Euler(0, 0, 0));
            listaBalas.Add(balaIns);
            balaIns.transform.parent = BalasHier.transform;
            balaIns.name = "Bala0";

            HingeJoint hj;
            hj = balaIns.GetComponent<HingeJoint>();
            hj.connectedBody = pistola.GetComponent<Rigidbody>();
            Debug.Log(hj.connectedBody);
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                int tamanoLista = listaBalas.Count;
                GameObject ultimoNodoLista = listaBalas[tamanoLista - 1];
                Vector3 posUltimaBala = ultimoNodoLista.transform.position;
                Quaternion ultimaRot = ultimoNodoLista.transform.rotation;

                posUltimaBala.y -= 0.75f;

                GameObject balaIns = Instantiate(bala, posUltimaBala, ultimaRot);

                HingeJoint hj = balaIns.GetComponent<HingeJoint>();
                hj.connectedBody = ultimoNodoLista.GetComponent<Rigidbody>();

                listaBalas.Add(balaIns);
            }
        }
    }
}
