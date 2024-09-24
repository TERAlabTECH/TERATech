using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selectorDeNiveles;
    public GameObject menuPrincipal;

    
    // Start is called before the first frame updat
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbreSelectorDeNiveles(){

        menuPrincipal.SetActive(false);
        selectorDeNiveles.SetActive(true);
    }
    public void OpenScene(){
        string sceneName="Juego";
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            // SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Please check if the scene name is correct and added in build settings.");
        }

    }
}
