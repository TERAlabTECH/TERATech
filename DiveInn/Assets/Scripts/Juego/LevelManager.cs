using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject aguaEffect;
    public GameObject menuPrincipal;
    public GameObject gameOverOxigenoUI;
    public GameObject gameOverCoralesUI;
    public GameObject diver;

    public int coralesLastimados=0;
    
    public bool paused=false;

    public GameObject DiverCam;
    public GameObject GalleryCam;

    public GameObject DiverCanvas;
    public GameObject GalleryCanvas;
    public bool diverCamBool=true;

    [Range(1,240)]public float tiempoInicialDeOxigeno=180f;
    public float porcentajeDeOxigeno;
    void Start()
    {   

        coralesLastimados=0;

        aguaEffect.SetActive(true);
        SetDiversPosition();
        
        
    }


    // Update is called once per frame
    void Update()
    {
        if(porcentajeDeOxigeno<=1){
            OpenOxigenoGameOver();
        }
        if(!gameOverOxigenoUI.activeSelf && coralesLastimados>=5){
            OpenCoralesGameOver();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("spaceBar");
            SwitchCameras();
            // Add additional actions you want to perform when space is pressed
        }

    }
    void OnApplicationQmenuPrincipalt(){
        aguaEffect.SetActive(false);

    }
    public void TogglemenuPrincipal(){
        if(menuPrincipal.activeSelf){ 
            menuPrincipal.SetActive(false);
            paused=false;
        }else{
            if(!gameOverOxigenoUI.activeSelf && !gameOverCoralesUI.activeSelf){
                menuPrincipal.SetActive(true);
                paused=true;
            }
        }

        

    
    }
    public void OpenInicio(){
        SceneManager.LoadScene("Inicio");
    }
    public void OpenOxigenoGameOver(){
        if(!gameOverOxigenoUI.activeSelf){
            menuPrincipal.SetActive(false);
            gameOverOxigenoUI.SetActive(true);
            paused=true;
        }
    }

    public void OpenCoralesGameOver(){
       if(!gameOverCoralesUI.activeSelf){
            menuPrincipal.SetActive(false);
            gameOverCoralesUI.SetActive(true);
            paused=true;
       } 
    }


    public static event Action OnRestart;
    public void ReStart(){

        OnRestart?.Invoke();
        paused=false;
        gameOverCoralesUI.SetActive(false);
        gameOverOxigenoUI.SetActive(false);

        porcentajeDeOxigeno=100;

        Start();
    }
    public void SetDiversPosition(){
        diver.GetComponent<Controller>().SetToInitalPosition();
    }
    public void LastimoCoral(){

        if(coralesLastimados<5){
            coralesLastimados++;
            Debug.Log($"lastimo {coralesLastimados} corales");
            if(coralesLastimados==1 || coralesLastimados==2 || coralesLastimados==4){
                ShowInfoCorales();
            }else{
                ShowWarningCorales();

            }

        }else{
           OpenCoralesGameOver();
        }
        
    }
    public void ShowInfoCorales(){
        paused=true;
        foreach (GameObject info in infoCorales)
        {
            info.SetActive(false);
        }

        // Create an instance of the Random class
        System.Random randm = new System.Random();
        // Generate a random index based on the number of items in your list
        int randomIndex = randm.Next(infoCorales.Count);

        // Activate the randomly selected coral info panel
        infoCorales[randomIndex].SetActive(true);
    }
    
    public void ShowWarningCorales(){

    } 

    public void SwitchCameras(){
        if(DiverCam.activeSelf){
            paused=true;
            DiverCanvas.SetActive(false);
            GalleryCanvas.SetActive(true);
            diverCamBool=false;
            DiverCam.SetActive(false);
            GalleryCam.SetActive(true);
        }else{
            HideInfoPeces();
            paused=false;
            GalleryCanvas.SetActive(false);
            DiverCanvas.SetActive(true);
            diverCamBool=true;
            GalleryCam.SetActive(false);
            DiverCam.SetActive(true);
        }
    }

    [SerializeField] List<GameObject> modelosInfoPeces;
    public void HideInfoPeces(){
        foreach(GameObject info in modelosInfoPeces){
            info.SetActive(false);
        }
    }
    [SerializeField] List<GameObject> infoCorales;
    public void HideInfoCorale(){
        paused=false;
        foreach(GameObject info in infoCorales){
            info.SetActive(false);
        }
    }

}
