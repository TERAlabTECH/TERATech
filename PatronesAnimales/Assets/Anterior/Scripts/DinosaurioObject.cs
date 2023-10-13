using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurioObject : MonoBehaviour
{
    private GameObject _personajeObject;

    public void SetObject(GameObject newObject) {

        //Agrego el nuevo objeto
        _personajeObject = Instantiate(newObject, this.transform); //Coloca el obj nuevo como hijo
    }
}
