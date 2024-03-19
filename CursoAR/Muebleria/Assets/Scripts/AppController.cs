using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppController : MonoBehaviour
{
    [Header("Furniture")]
    public int startIndex = 0;
    public FurnitureObject furnitureObject;

    [Header("UI")]
    public TextMeshProUGUI titleTxt;
    public Furniture furniturePrefab; //El btn
    public Transform furnitureContainer; //El container del scroll-view


    [Header("Data")]
    public FurnitureSO[] data; //Un arreglo de todos los modelos (scriptable Objects) que hay en el proyecto

    private Furniture _furniture;

    private void Start()
    {
        CreatePrefabs(); //Instancia todos los btns
        ChangeFurniture(data[startIndex]);
    }

    //Crea un btn por cada obj
    private void CreatePrefabs() {
        for(int i=0; i<data.Length; i++) {
            _furniture = Instantiate(furniturePrefab, furnitureContainer); // instancio el obj en el scroll view
            _furniture.Init(data[i]); //Guarda toda la info del SO
            int index = i; //para evitar el error de usar una funcion lambda dentro de un ciclo
            _furniture.SetButton(() => ChangeFurniture(data[index])); //Se le otorga la funcionalidad al btn
        }
    }

    //Se llama cada vez que se toca el btn
    private void ChangeFurniture(FurnitureSO furnitureSO) {
        titleTxt.text = furnitureSO.title; //Cambia el titulo en la UI
        furnitureObject.SetObject(furnitureSO.prefab); //El obj que esta almacenado en el scriptable object se agrega 

    }
}
