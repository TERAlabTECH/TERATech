using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject infoCollectible;
    public GameObject modelo3dCollectible;
    
    //Se conecta con el elemento de canvas que tiene la info
    public void ShowCollectibleInfo(){
        infoCollectible.SetActive(true);
        modelo3dCollectible.SetActive(true);
    }

}
