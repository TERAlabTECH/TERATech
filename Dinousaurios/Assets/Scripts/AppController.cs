using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AppController : MonoBehaviour
{
    [Header("Dinosaurio")]
    public int startIndex = 0;
    public DinosaurioObject dinosaurioObject;

    [Header("UI")]
    public TextMeshProUGUI titleTxt; //El nombre del dinosaurio
    public Dinosaurio dinousaurioPrefab; //El btn
    public TipoDinosaurio tipoDinousaurioPrefab; //El btn
    public Transform dinosaurioContainer; //El container del scroll-view
    public Transform tipoDinosaurioContainer; //El container del scroll-view


    [Header("Data")]
    public DinosaurioSO[] dinoPiel; //Un arreglo de todos los modelos (scriptable Objects) que hay en el proyecto
    public DinosaurioSO[] dinoHueso; //Un arreglo de todos los modelos (scriptable Objects) que hay en el proyecto
    public TipoDinosaurioSO[] tipoDinoBtn;

    private Dinosaurio _dinosaurio;
    private TipoDinosaurio _tipoDinosaurio;
    private Boolean _esEsqueleto = false;
    private Boolean _primerClick = false;

    private void Start()
    {
        CreatePrefabs(0); //Instancia todos los btns
        ChangeDinosaurio(dinoPiel[startIndex]);

    }

    //Crea un btn por cada obj
    private void CreatePrefabs(int tipoDino) {
        if (tipoDino == 0) {
            for (int i = 0; i < dinoPiel.Length; i++)
            {
                _dinosaurio = Instantiate(dinousaurioPrefab, dinosaurioContainer); // instancio el obj en el scroll view
                _dinosaurio.Init(dinoPiel[i]); //Guarda toda la info del SO
                int index = i; //para evitar el error de usar una funcion lambda dentro de un ciclo
                _dinosaurio.SetButton(() => ChangeDinosaurio(dinoPiel[index])); //Se le otorga la funcionalidad al btn
            }
        } else if (tipoDino == 1) {
            for (int i = 0; i < tipoDinoBtn.Length; i++)
            {
                _tipoDinosaurio = Instantiate(tipoDinousaurioPrefab, tipoDinosaurioContainer); // instancio el obj en el scroll view
                _tipoDinosaurio.Init(tipoDinoBtn[i]); //Guarda toda la info del SO
                int index = i; //para evitar el error de usar una funcion lambda dentro de un ciclo
            }
        }
        
    }

    //Se llama cada vez que se toca el btn
    private void ChangeDinosaurio(DinosaurioSO dinosaurioSO) {
        titleTxt.text = dinosaurioSO.nombre; //Cambia el titulo en la UI
        dinosaurioObject.SetObject(dinosaurioSO.prefab); //El obj que esta almacenado en el scriptable object se agrega 
        if (_primerClick) {
            CreatePrefabs(1);
            _primerClick = false;
        }
        _primerClick = true;
    }

    private void ChangeTipoDinosaurio(DinosaurioSO dinosaurioSO) {
        dinosaurioObject.SetObject(dinosaurioSO.prefab); //El obj que esta almacenado en el scriptable object se agrega
        if (_primerClick) {
            CreatePrefabs(1);
        }
    }
}
