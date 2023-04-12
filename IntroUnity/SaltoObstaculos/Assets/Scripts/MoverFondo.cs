using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFondo : MonoBehaviour
{
    private float vel = 0.0f;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate() //la velocidad con la que corre es diferente dependiendo de la computadora --> para eso el FixedUpdate, para que no dependa de los refrescamientos
    {
        Vector2 offset = new Vector2(Time.time * vel, 0); //El tiempo que ha pasado por la velocidad (que tan rápido vamos a mover la tectura)
        rend.material.mainTextureOffset = offset; //En que direccion muevo la textura
    }
}
