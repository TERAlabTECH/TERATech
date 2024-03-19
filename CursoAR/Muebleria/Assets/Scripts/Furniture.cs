
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Furniture : MonoBehaviour
{
    public TextMeshProUGUI furnitureTitle;
    public Image furnitureImg;
    public Button furnitureBtn;

    // Recibe un objeto FurnitureSO que contiene la información del mueble.
    public void Init(FurnitureSO furnitureSO)
    {
        furnitureTitle.text = furnitureSO.title;
        furnitureImg.sprite = furnitureSO.furnitureImg;
    }

    // Método para agregar un evento al botón del mueble.
    // Recibe una acción de Unity (callback) que se ejecutará cuando se haga clic en el botón.
    public void SetButton(UnityAction callback) {
        furnitureBtn.onClick.AddListener(callback);
    }
}
