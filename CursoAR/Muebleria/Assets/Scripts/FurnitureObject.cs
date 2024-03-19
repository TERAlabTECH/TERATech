using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureObject : MonoBehaviour
{
    private GameObject _furnitureObject;

    public void SetObject(GameObject newObject) {
        //Destruyo el objeto anterior
        Destroy(_furnitureObject);

        //Agrego el nuevo objeto
        _furnitureObject = Instantiate(newObject, this.transform); //Coloca el obj nuevo como hijo
    }
}
