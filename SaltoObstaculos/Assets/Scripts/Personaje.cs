using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    private bool estaEnPiso;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        estaEnPiso = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (estaEnPiso) {
            if (Input.GetButtonDown("Jump")) {
                estaEnPiso = false;
            }
        } else {
            anim.SetBool("estaSaltando", true);
        }
    }
}
