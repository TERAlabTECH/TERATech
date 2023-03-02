using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private GameObject enemigo;//GameObject agarra toda la información de un objeto de la escena -- Enemigo e agarraría solo la clase de enemigo
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> lista_enemigos = new List<GameObject>();

    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(enemigo, transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0, 0, 0)); //Instancia un objeto de tipo enemigo en la posición de la esfera + 5 en y, no lo rota.

        }


        //if (Input.GetButtonDown("Jump")) //Checa que la tecla está asignada a 'Jump' (en input dentro de project settings) se haya hecho click
        //{
        //    Instantiate(enemigo, transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0, 0, 0)); //Instancia un objeto de tipo enemigo en la posición de la esfera + 5 en y, no lo rota.

        //}
    }

    //Cuando el mouse está haciendo click
    private void OnMouseDown()
    {
        ExplotarEnemigos();
    }

    public void ExplotarEnemigos()
    {
        //enemigo.Explotar(pos)
    }
}
