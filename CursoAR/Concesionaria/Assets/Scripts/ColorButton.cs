using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    private Image _colorImg;
    private Button _colorBtn;

    private void Awake()
    {
        _colorBtn = GetComponent<Button>();
        _colorImg = GetComponent<Image>();

    }

    private void Start()
    {
        //Manda a llamar la funcion del GameManager para cambiar el color
        //manda como parametro a esa funcion el color del boton al que se le dio click
        _colorBtn.onClick.AddListener(() => GameManager.Instance.CambiarColor(_colorImg.color));
        _colorBtn.onClick.AddListener(() => Debug.Log(_colorImg.color));
    }
}
