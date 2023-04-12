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
        } else if (Input.GetButtonDown("Fire1"))
        {
            if(listaBalas.Count > 0)
            {
                dispararBala();
            } else
            {
                Debug.Log("No hay balas!");
            }
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

                posUltimaBala.y -= ultimoNodoLista.transform.localScale.y;

                GameObject balaIns = Instantiate(bala, posUltimaBala, ultimaRot);

                HingeJoint hj = balaIns.GetComponent<HingeJoint>();
                hj.connectedBody = ultimoNodoLista.GetComponent<Rigidbody>();

                listaBalas.Add(balaIns);
                balaIns.transform.parent = BalasHier.transform;
                balaIns.name = "Bala "+(tamanoLista-1);
            }
        }
    }

    //Quita la vala y la dispara
    public void dispararBala()
    {
        Destroy(listaBalas[listaBalas.Count-1]);
        listaBalas.RemoveAt(listaBalas.Count-1);

        GameObject balaIns = Instantiate(bala, pistola.transform.position + new Vector3(0, 0, 1), Quaternion.Euler(90, 0, 00));
        balaIns.transform.localScale = new Vector3(balaIns.transform.localScale.x + 0.5f, balaIns.transform.localScale.y + 0.5f, balaIns.transform.localScale.z + 0.5f);
        Destroy(balaIns.GetComponent<HingeJoint>());
        Rigidbody rb = balaIns.GetComponent<Rigidbody>();
        rb.AddForce(0, 0, 70, ForceMode.Impulse);

    }
}
