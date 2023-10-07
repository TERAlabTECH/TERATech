using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppController : MonoBehaviour
{
    [Header("Dinosaurio")]
    public int startIndex = 0;
    public DinosaurioObject dinosaurioObject;

    [Header("UI")]
    public TextMeshProUGUI titleTxt;
    public Dinosaurio dinousaurioPrefab; //El btn
    public Transform dinosaurioContainer; //El container del scroll-view


    [Header("Data")]
    public DinosaurioSO[] data; //Un arreglo de todos los modelos (scriptable Objects) que hay en el proyecto

    private Dinosaurio _dinosaurio;

    private void Start()
    {
        CreatePrefabs(); //Instancia todos los btns
        ChangeDinosaurio(data[startIndex]);
    }

    //Crea un btn por cada obj
    private void CreatePrefabs() {
        for(int i=0; i<data.Length; i++) {
            _dinosaurio = Instantiate(dinousaurioPrefab, dinosaurioContainer); // instancio el obj en el scroll view
            _dinosaurio.Init(data[i]); //Guarda toda la info del SO
            int index = i; //para evitar el error de usar una funcion lambda dentro de un ciclo
            _dinosaurio.SetButton(() => ChangeDinosaurio(data[index])); //Se le otorga la funcionalidad al btn
        }
    }

    //Se llama cada vez que se toca el btn
    private void ChangeDinosaurio(DinosaurioSO dinosaurioSO) {
        titleTxt.text = dinosaurioSO.nombre; //Cambia el titulo en la UI
        dinosaurioObject.SetObject(dinosaurioSO.prefab); //El obj que esta almacenado en el scriptable object se agrega 

    }
}
