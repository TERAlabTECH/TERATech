using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public GameObject levelManager;
    public LevelManager lvlManager;
    float tiempoDeOxigeno;

    public GameObject aguaShader;
    private SpriteRenderer aguaShaderRenderer; // To store the material of the water shader
    private Color originalColor; // Store the original color of the shader
    private Color LowOxygenColor=new Vector4(0.8486567f, 0.5880502f,1,1);

    Image imagen;
    Material oxygenBar;
    float percentage = 100;
    private float blinkTimer = 0f; // Timer for blinking
    private bool isRed = false; // To track if the shader is currently red

    void Start()
    {
        lvlManager = levelManager.GetComponent<LevelManager>();
        imagen = GetComponent<Image>();

        tiempoDeOxigeno = lvlManager.tiempoInicialDeOxigeno;

        percentage = 100;
        oxygenBar = imagen.materialForRendering;

        aguaShaderRenderer = aguaShader.GetComponent<SpriteRenderer>(); // Get the shader's material
        originalColor = aguaShaderRenderer.color; // Store the original color
    }

    void Update()
    {
        if (!lvlManager.paused)
        {
            percentage -= (100f / tiempoDeOxigeno) * Time.deltaTime;
            oxygenBar.SetFloat("_AlphaCutoff", percentage);
            lvlManager.porcentajeDeOxigeno = percentage;

            BlinkAguaShader();

            if (percentage <= (100f / tiempoDeOxigeno) * 5f) // If there are 5 seconds or less remaining
            {
            }
            else
            {
                ResetAguaShaderColor(); // Reset to original if more than 5 seconds remain
            }
        }
    }

    void BlinkAguaShader()
    {
        aguaShaderRenderer.color=LowOxygenColor;
        blinkTimer += Time.deltaTime;

        if (blinkTimer >= 0.5f) // Change color every 0.5 seconds
        {
            isRed = !isRed; // Toggle the color state
            if(isRed){
                
                aguaShaderRenderer.color=LowOxygenColor;
            }else{
                aguaShaderRenderer.color=originalColor;
            }
            blinkTimer = 0f; // Reset the timer
        }
    }

    void ResetAguaShaderColor()
    {
        aguaShaderRenderer.color=originalColor; // Ensure the shader returns to its original color
        isRed = false;
    }

    void OnApplicationQuit()
    {
        percentage = 100;
    }

    void OnEnable()
    {
        LevelManager.OnRestart += Start;
    }

    void OnDisable()
    {
        LevelManager.OnRestart -= Start;
    }
}
