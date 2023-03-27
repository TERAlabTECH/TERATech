using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    private GameController gc;
    [SerializeField] private GameObject obstaculo;
    [SerializeField] private List<GameObject> listaObstaculos = new List<GameObject>();
    private GameObject obstaculosHier;

    // Start is called before the first frame update
    void Start()
    {
        obstaculosHier = new GameObject();
        obstaculosHier.name = "Obstaculos";
        gc = GetComponent<GameController>();
        StartCoroutine(agregarObstaculo());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moverObstaculos();
    }

    IEnumerator agregarObstaculo() //corrutinas
    {
        while (gc.EstaJugando)
        {
            yield return new WaitForSeconds(Random.Range(1,4)); //Todo IEnumerator necesita esto
            GameObject nuevoObstaculo = Instantiate(obstaculo, new Vector3(10, -7.04f, -1.75f), Quaternion.identity);
            nuevoObstaculo.name = "Obstaculo";
            nuevoObstaculo.transform.parent = obstaculosHier.transform;
            listaObstaculos.Add(nuevoObstaculo);
        }

    }

    public void moverObstaculos()
    {
        if(listaObstaculos.Count > 0) {
            for(int i=0; i < listaObstaculos.Count; i++) {
                listaObstaculos[i].transform.Translate(Time.deltaTime * -9, 0, 0); //deltaTime es el tiempo entre un frame y otro
                if (listaObstaculos[i].transform.position.x <= -10.54f)
                {
                    Destroy(listaObstaculos[i]);
                    listaObstaculos.RemoveAt(i);
                }
            }
        }
    }

    public void Reset() {
        if (listaObstaculos.Count > 0)
        {
            for (int i = 0; i < listaObstaculos.Count; i++)
            {
                    Destroy(listaObstaculos[i]);
                    listaObstaculos.RemoveAt(i);
            }
            //listaObstaculos.Clear();
        }

        
    }

}
