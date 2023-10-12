using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Nuevo Tipo de Dinosaurio", menuName = "Tipo Dinosaurio", order = 1)]
public class TipoDinosaurioSO : ScriptableObject
{
    public string nombre;
    public Sprite imgDinosaurio;
}