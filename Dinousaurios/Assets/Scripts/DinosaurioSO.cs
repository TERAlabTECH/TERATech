using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Dinosaurio", order = 0)]
public class DinosaurioSO : ScriptableObject
{
    public string nombre;
    public Sprite imgDinosaurio;
    public GameObject prefab;
}