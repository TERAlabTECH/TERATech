using System.Collections.Generic; // Para usar listas.
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Asegúrate de incluir TextMeshPro
using UnityEngine.SceneManagement;
using System.Collections;
public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText; // Texto de la pregunta.
        public string[] options; // Opciones de respuesta.
        public int correctAnswerIndex; // Índice de la respuesta correcta.
    }

    public TextMeshProUGUI questionText; // Referencia al TextMeshPro que mostrará la pregunta.
    public GameObject[] answerButtons; // Referencias a los botones con TextMeshPro para las respuestas.
    public List<Question> questions; // Lista de preguntas.
    private int currentQuestionIndex = 0; // Índice de la pregunta actual.
    public static int playerMoney = 0; // Dinero del jugador.
    public HealthManager healthManager; // Referencia al script HealthManager.
    public GameObject background; // Fondo que cambiará de color.
    public GameObject quizPanel; // Panel donde se muestra la pregunta y las opciones.
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        // Ocultamos el panel de preguntas al inicio.
        //quizPanel.SetActive(false);
        //ShowQuestion(); // Llamar a la función que muestra la primera pregunta.
    }

    public void ShowQuestion()
    {
        // Mostrar el panel de preguntas.
        quizPanel.SetActive(true);
        // Cargar la primera pregunta.
        LoadQuestion();
    }

    public void LoadQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            // Obtener la pregunta actual.
            Question currentQuestion = questions[currentQuestionIndex];

            // Mostrar el texto de la pregunta.
            questionText.text = currentQuestion.questionText;

            // Asignar las opciones a los botones.
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.options.Length)
                {
                    // Habilitar el botón y asignar el texto de la opción.
                    answerButtons[i].SetActive(true);
                    TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.text = currentQuestion.options[i];
                    }

                    // Agregar el evento de respuesta al botón.
                    //int index = i; // Evitar problemas de captura de variables en lambdas.
                    //Button button = answerButtons[i].GetComponent<Button>();
                    //Debug.Log(i);
                    //if (button != null)
                    //{
                    //    button.onClick.RemoveAllListeners();
                    //    button.onClick.AddListener(() => CheckAnswer(i));
                    //}
                }
                else
                {
                    // Deshabilitar botones adicionales si no hay más opciones.
                    answerButtons[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("No hay más preguntas.");
            quizPanel.SetActive(false);
        }
    }
    public void OnAnswerButtonClick(int answerIndex)
    {
            CheckAnswer(answerIndex);
            Debug.Log("click");
    }
   // void Awake()
   // {
   //     DontDestroyOnLoad(this.gameObject);
  //  }
    private void CheckAnswer(int selectedIndex)
    {
        Debug.Log("pepepe");
        // Verificar si la respuesta es correcta.
        bool isAnswerCorrect = selectedIndex == questions[currentQuestionIndex].correctAnswerIndex;

        if (isAnswerCorrect)
        {
            // Respuesta correcta.
            Debug.Log("Respuesta correcta!");
            playerMoney += 10; // Aumentar el dinero del jugador.
            // Actualizar el texto con el nuevo valor de dinero
            moneyText.text = "Puntos: " + playerMoney;
            // Actualizar la interfaz si hay más preguntas.

            if (currentQuestionIndex <= questions.Count)
            {
                currentQuestionIndex++; // Avanzar a la siguiente pregunta.
            }
            else
            {
                Debug.Log("Has respondido todas las preguntas.");
            }
            quizPanel.SetActive(false);
            healthManager.ResetHealth();
        }
        else
        {
            Debug.Log("Respuesta incorrecta.");
            playerMoney -= 5;
            // Actualizar el texto con el nuevo valor de dinero
            moneyText.text = "Puntos: " + playerMoney;
            StartCoroutine(ChangeBackgroundColor()); // Llamar a la corutina para cambiar el color del fondo.
        }
    }

    private IEnumerator ChangeBackgroundColor()
    {
        Image bgImage = background.GetComponent<Image>();

        if (bgImage != null)
        {
            Color originalColor = bgImage.color;
            Color incorrectColor = new Color(1f, 0f, 0.13f); // FF0021
            Color originalHexColor = new Color(0.49f, 0f, 0.42f); // 7C006C

            // Cambiar al color de respuesta incorrecta.
            bgImage.color = incorrectColor;

            // Esperar medio segundo.
            yield return new WaitForSeconds(0.5f);

            // Volver al color original.
            bgImage.color = originalHexColor;
        }
    }

}