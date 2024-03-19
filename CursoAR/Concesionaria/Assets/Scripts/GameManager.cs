using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Material materialPaint;

    private void Awake()
    {
        if (Instance == null ) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
    }

    public void CambiarColor(Color nuevoColor) {
        Debug.Log(materialPaint.GetColor("_BaseColor"));
        materialPaint.SetColor("_BaseColor", nuevoColor); //El nombre de la propiedad esta en el shader del material
    }
}
