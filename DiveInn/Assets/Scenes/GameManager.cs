using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject selectorDeNiveles;
    public GameObject menuPrincipal;
    public GameObject aguaShader;

    public float tiempoDeOxigeno;

    bool paused=false;

    
    // Start is called before the first frame updat
    void Start()
    {
        aguaShader.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(paused!){

        }
    }

    public void AbreSelectorDeNiveles(){

        menuPrincipal.SetActive(false);
        selectorDeNiveles.SetActive(true);
    }
    public void AbreMenu(){
        selectorDeNiveles.SetActive(false);
        menuPrincipal.SetActive(true);
    }
    public void OpenNivel1(){
        SceneManager.LoadScene("Nivel1");
    }
}
