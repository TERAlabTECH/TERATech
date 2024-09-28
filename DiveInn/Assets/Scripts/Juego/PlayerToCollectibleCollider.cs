using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerToCollectibleCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelManager lvlManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("collectibles"))
        {
            other.gameObject.GetComponent<Collectible>().ShowCollectibleInfo();
            
            lvlManager.SwitchCameras();

           
            other.gameObject.SetActive(false);
        }
    }
  
}
