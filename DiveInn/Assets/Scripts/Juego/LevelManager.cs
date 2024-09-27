using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject aguaEffect;
    public GameObject UI;
    public bool paused=false;
    void Start()
    {
        aguaEffect.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void OnApplicationQuit(){
        aguaEffect.SetActive(false);

    }
    public void ToggleUi(){
        if(UI.activeSelf){ 
            UI.SetActive(false);
            paused=false;
        }else{
            UI.SetActive(true);
            paused=true;
        }

    
    }
    public void OpenInicio(){
        SceneManager.LoadScene("Inicio");
    }
}
