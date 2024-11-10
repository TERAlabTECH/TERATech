using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // La nave espacial a seguir
    public Vector3 offset;   // Desplazamiento detrás de la nave espacial

    void LateUpdate()
    {
        // Mantén la cámara en la posición correcta detrás de la nave espacial
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}