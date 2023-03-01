using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private Enemigo enemigo; //Solo agarra la clase
    //GameObject agarra toda la información de un objeto de la escena 
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cuando el mouse está haciendo click
    private void OnMouseDown()
    {
        ExplotarEnemigos();
    }

    public void ExplotarEnemigos()
    {
        enemigo.Explotar(pos);
    }
}
