using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject aguaEffect;
    public GameObject menuPrincipal;
    public GameObject gameOverOxigenoUI;
    public GameObject gameOverCoralesUI;
    public GameObject diver;
    public GameObject infoCorales1;
    public GameObject infoCorales2;

    public int coralesLastimados=0;
    
    public bool paused=false;

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
            if(coralesLastimados==1){
                infoCorales1.SetActive(true);
            }else if (coralesLastimados==4){
                infoCorales2.SetActive(true);
            }else{
                ShowWarningCorales();

            }

        }else{
           OpenCoralesGameOver();
        }
        
    }
    public void ShowInfoCorales(){
        Debug.Log("info corales display");
        infoCorales1.SetActive(true);
    }
    public void ShowWarningCorales(){

    } 

}
