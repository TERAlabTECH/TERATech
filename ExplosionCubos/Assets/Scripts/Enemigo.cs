using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo: MonoBehaviour
{

    [SerializeField] private new Rigidbody rigidbody; //SerializeField permite que se vea la variable privada en el editor de Unity
    private Renderer rend;
    private Explosion ex; //Para poder hacer referencia a esta clase

    public Rigidbody Rigidbody {
        get => rigidbody;
        set => rigidbody = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        float randomEscala = Random.Range(0.1f, 1.5f);
        rigidbody = GetComponent<Rigidbody>(); //Busca en el objeto la variable que puse en <>
        rend = GetComponent<Renderer>(); //El mesh render es el que controla el material
        ex = GameObject.Find("Sphere").GetComponent<Explosion>();

        rend.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)); //Cada que se genera un enemigo va a tener un color diferente
        rigidbody.transform.localScale = new Vector3(randomEscala, randomEscala, randomEscala);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Explotar(Vector3 pos)
    {
        rigidbody.AddExplosionForce(10, pos, 100, 10, ForceMode.Impulse);
    }

    private void OnMouseDown()
    {
        EliminarEnemigos();
    }
    private void EliminarEnemigos()
    {
        int index = ex.Lista_enemigos.IndexOf(gameObject);
        
        Destroy(ex.Lista_enemigos[index]);
        ex.Lista_enemigos.RemoveAt(index);
    }
}
