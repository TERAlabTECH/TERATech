
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TipoDinosaurio : MonoBehaviour
{
    public TextMeshProUGUI tipoModelo;
    public Image imgTipo;
    public Button btn;

    // Recibe un objeto FurnitureSO que contiene la información del mueble.
    public void Init(TipoDinosaurioSO tipoDinosaurioSO)
    {
        tipoModelo.text = tipoDinosaurioSO.nombre;
        imgTipo.sprite = tipoDinosaurioSO.imgDinosaurio;
    }

    // Método para agregar un evento al botón del mueble.
    // Recibe una acción de Unity (callback) que se ejecutará cuando se haga clic en el botón.
    public void SetButton(UnityAction callback) {
        btn.onClick.AddListener(callback);
    }
}
