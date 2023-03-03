using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private GameObject enemigo;//GameObject agarra toda la información de un objeto de la escena -- Enemigo e agarraría solo la clase de enemigo
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> lista_enemigos = new List<GameObject>();
    private GameObject enemigoJerarquia;

    public List<GameObject> Lista_enemigos {
        get => lista_enemigos;
        set => lista_enemigos = value;
    }

    void Start()
    {
        pos = transform.position;
        enemigoJerarquia = new GameObject();
        enemigoJerarquia.name = "Grupo de enemigos";
    }

    // Update is called once per frame
    void Update()
    {
        CrearEnemigo();
        VerificarAltura();
    }

    private void CrearEnemigo()
    {
        GameObject nuevoEnemigo;
        if (Input.GetButtonDown("Jump"))
        {
            nuevoEnemigo = Instantiate(enemigo, transform.position + new Vector3(Random.Range(-8.5f,8.5f),15, Random.Range(-7.1f, 5.9f)), Quaternion.Euler(0, 0, 0)); //Instancia un objeto de tipo enemigo en la posición aleatorio y no lo rota.
            Lista_enemigos.Add(nuevoEnemigo.gameObject);
            nuevoEnemigo.transform.parent = enemigoJerarquia.gameObject.transform; //Lo pone dentro de una jerarquia

        }
    }
    //Cuando el mouse está haciendo click
    private void OnMouseDown()
    {
        ExplotarEnemigos();
    }

    public void ExplotarEnemigos()
    {
        foreach (var enemigo in Lista_enemigos)
        {
           Enemigo e = enemigo.GetComponent<Enemigo>(); //por cada elemento de la lista busco su componente enemigo (que es el script)
            e.Explotar(pos);
        }
    }

    //Cuando la altura de un enemigo es menor a x, vuelve a caer
    public void VerificarAltura()
    {
        foreach (var enemigo in Lista_enemigos)
        {
            if(enemigo.transform.position.y < -40)
            {
                enemigo.transform.position = new Vector3(pos.x, pos.y + 15, pos.z);
            }
        }
    }
}
