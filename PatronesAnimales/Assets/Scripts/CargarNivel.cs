using UnityEngine;
using UnityEngine.SceneManagement;

//Script para pasar de un nivel a otro. Este script va dentro del Game Controller
public class CargarNivel : MonoBehaviour
{
    //En la primera escena, en al canvas, hay un botón en el que su OnClick function es este método
    //Depende de qué botón sea es con qué valor de n manda a llamar al método
    public void cargarNivel(int n)
    {
        if (n == 1) { 
            SceneManager.LoadScene("Nivel1");
        }
        else if (n == 2) //Actualmente en el canvas no hay botón de N.2 pq todavía no está listo
        //Cuando lo haya, solo hay que cambiar el parámetro del OnClick a 2
        {
            SceneManager.LoadScene("Nivel2");
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