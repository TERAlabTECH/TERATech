
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Dinosaurio : MonoBehaviour
{
    public TextMeshProUGUI nombreDinosaurio;
    public Image imgDinosaurio;
    public Button dinosaurioBtn;

    // Recibe un objeto FurnitureSO que contiene la información del mueble.
    public void Init(DinosaurioSO dinosaurioSO)
    {
        nombreDinosaurio.text = dinosaurioSO.nombre;
        imgDinosaurio.sprite = dinosaurioSO.imgDinosaurio;
    }

    // Método para agregar un evento al botón del mueble.
    // Recibe una acción de Unity (callback) que se ejecutará cuando se haga clic en el botón.
    public void SetButton(UnityAction callback) {
        dinosaurioBtn.onClick.AddListener(callback);
    }
}
