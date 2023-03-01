using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo: MonoBehaviour
{

    [SerializeField] private new Rigidbody rigidbody; //SerializeField permite que se vea la variable privada en el editor de Unity

    public Rigidbody Rigidbody {
        get => rigidbody;
        set => rigidbody = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); //Busca en el objeto la variable que puse en <>
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explotar(Vector3 pos)
    {
        rigidbody.AddExplosionForce(10, pos, 100, 10, ForceMode.Impulse);
    }
}
