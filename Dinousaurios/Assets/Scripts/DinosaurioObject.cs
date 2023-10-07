using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurioObject : MonoBehaviour
{
    private GameObject _dinosaurioObject;

    public void SetObject(GameObject newObject) {
        //Destruyo el objeto anterior
        Destroy(_dinosaurioObject);

        //Agrego el nuevo objeto
        _dinosaurioObject = Instantiate(newObject, this.transform); //Coloca el obj nuevo como hijo
    }
}
