using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarNivel : MonoBehaviour
{
    public void cargarNivel(int n)
    {
        if (n == 1) {
            SceneManager.LoadScene("Nivel1");
        }
        
    }

    public void nivelGanado() {
        SceneManager.LoadScene("InterfazTrancision");
    }

    public void pantallaDeInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}