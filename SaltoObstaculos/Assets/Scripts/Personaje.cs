using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    private bool estaEnPiso;
    private Animator anim;
    private Rigidbody rb;
    private int fuerzaSalto;
    private Vector3 fuerza;
    // Start is called before the first frame update
    void Start()
    {
        estaEnPiso = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        fuerzaSalto = 1400;
        fuerza = new Vector3(0, fuerzaSalto, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (estaEnPiso) {
            if (Input.GetButtonDown("Jump")) {
                estaEnPiso = false;
                rb.AddForce(fuerza);
            }
        } else {
            anim.SetBool("estaSaltando", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piso")) {
            estaEnPiso = true;
            anim.SetBool("estaSaltando", false);
        }
    }
}
