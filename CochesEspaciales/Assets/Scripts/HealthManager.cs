using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts; // Drag your heart UI Images into this array
    public GameObject gameOverCanvas; // Assign the Game Over Canvas
    private int lives = 3; // Number of lives the player starts with
    private bool isGameOver = false;
    public MeteorSpawner meteoritos;
    public QuizManager quiz;

    void Start()
    {
        // Ensure Game Over canvas is disabled at the start
        gameOverCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver) // Only process collisions if the game isn't over
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        if (lives > 0)
        {
            lives--; // Decrease the number of lives
            StartCoroutine(AnimateHeartLoss(lives)); // Animate the heart loss
        }

        else
        {
            GameOver(); // Trigger Game Over when lives reach 0
        }
    }

    IEnumerator AnimateHeartLoss(int index)
    {
        // Play animation (e.g., fading out) for the heart at the given index
        Image heart = hearts[index];

        // Example: Fade the heart out
        float duration = 0.5f;
        Color originalColor = heart.color;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            heart.color = Color.Lerp(originalColor, Color.clear, t / duration);
            yield return null;
        }

        heart.color = Color.clear; // Ensure it's fully transparent
    }
    //void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}
    public void ResetHealth()
    {
        lives = 3; // Reiniciar las vidas
        isGameOver = false; // Asegurarse de que ya se procesan las colisiones otra vez

        // Reactivar las imágenes de las vidas
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = Color.white; // Restaurar el color original
        }

        //gameOverCanvas.SetActive(false); // Desactivar el panel del quiz
        meteoritos.setBandera(true);
    }

    private void GameOver()
    {
        isGameOver = true;
        meteoritos.setBandera(false);
        //gameOverCanvas.SetActive(true); // la variable se llama game over, pero pone la interfaz del quiz
        quiz.ShowQuestion();
    }
}