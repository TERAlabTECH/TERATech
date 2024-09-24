using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sss : MonoBehaviour
{

    public int numeracion=0;
    public GameObject circulo;

    public GameObject yoMismo;
    public String nombre; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other){

        other.GetComponent<SpriteRenderer>().color= new Vector4(0,0,1,1);
        // sp.color=new Vector4(0,0,1,1);
        // //Referencia a el objecto sobre el que esta el scipt
        SpriteRenderer spSelf= gameObject.GetComponent<SpriteRenderer>();
        spSelf.color=new Vector4(0,1,0,1);
        
    }
}
