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
    private int _primerClick = 0;

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
                _tipoDinosaurio.SetButton(() => ChangeTipoDinosaurio(tipoDinoBtn[index])); //Se le otorga la funcionalidad al btn

            }
        }
        
    }

    //Se llama cada vez que se toca el btn
    private void ChangeDinosaurio(DinosaurioSO dinosaurioSO) {
        titleTxt.text = dinosaurioSO.nombre; //Cambia el titulo en la UI
        dinosaurioObject.SetObject(dinosaurioSO.prefab); //El obj que esta almacenado en el scriptable object se agrega 
        if (_primerClick == 1) { //Para que los botones de tipo solo aparezcan despues de haber seleccionado el dinosaurio
            CreatePrefabs(1);
            _primerClick += 1;
        }
        _primerClick += 1;
    }

    //Falta hacerlo no dependiente de strings
    private void ChangeTipoDinosaurio(TipoDinosaurioSO tipoDinosaurioSO) {
        Debug.Log(tipoDinosaurioSO.nombre);

        if (tipoDinosaurioSO.nombre.Equals("Esqueleto")&&!_esEsqueleto) { //Estoy en tipo piel y quiero pasar a tipo esqueleto
            //Buscar una forma más eficiente de esto, para que no tenga que ser con strings
            if (titleTxt.text.Equals(dinoPiel[0].nombre)) { //Entonces esta seleccionado el primer dinosaurio
                ChangeDinosaurio(dinoHueso[0]);
            } else if (titleTxt.text.Equals(dinoPiel[1].nombre)) { //Entonces esta seleccionado el segundo dinosaurio
                ChangeDinosaurio(dinoHueso[1]);
            }
            else
            { //Entonces esta seleccionado el tercer dinosaurio
                ChangeDinosaurio(dinoHueso[2]);
            }
            _esEsqueleto = true; //para evitar re-renders inecesarios
        } else if (tipoDinosaurioSO.nombre.Equals("Piel") && _esEsqueleto) {
            if (titleTxt.text.Equals(dinoHueso[0].nombre))
            { //Entonces esta seleccionado el primer dinosaurio
                ChangeDinosaurio(dinoPiel[0]);
            }
            else if (titleTxt.text.Equals(dinoHueso[1].nombre))
            { //Entonces esta seleccionado el segundo dinosaurio
                ChangeDinosaurio(dinoPiel[1]);
            }
            else
            { //Entonces esta seleccionado el tercer dinosaurio
                ChangeDinosaurio(dinoPiel[2]);
            }
            _esEsqueleto = false;
        }
    }
}
