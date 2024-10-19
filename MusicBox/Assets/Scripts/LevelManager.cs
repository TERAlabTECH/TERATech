using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> luces;

   
    public GameObject scoreText;
    Secuencia sequence;
    InputSecuenia inputSequence;
    public UnityEvent sequenceCompleted;
    public GameObject fogatas;
    
    public GameObject nivelTexto;
    public GameObject mainMenu;
    public GameObject mainMenuText;
    void Start()
    {
        mainMenu.SetActive(true);
        fogatas.SetActive(true);
        sequenceCompleted.AddListener(()=>StartCoroutine(sequenceCompletedCoroutine()));
        sequence=GetComponent<Secuencia>();
        inputSequence=GetComponent<InputSecuenia>();
    }
    public void nextSequence(){
        sequenceCompleted.Invoke();
    }

    IEnumerator sequenceCompletedCoroutine(){
        SetNivelTexto();

        nivelTexto.SetActive(true);
        yield return new WaitForSeconds(3);
        nivelTexto.SetActive(false);
        sequence.triggerSequence();
    }
    public void gameOver(){
        mainMenuText.GetComponent<TMPro.TextMeshProUGUI>().SetText($"Llegaste al nivel {sequence.sequenceLenght}");
        mainMenu.SetActive(true);
        sequence.playingSequence=true;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    void SetNivelTexto(){
        nivelTexto.GetComponent<TMPro.TextMeshProUGUI>().SetText($"Nivel {sequence.sequenceLenght}");
    }

    public void Restart(bool imposible){
        inputSequence.Restart();
        sequence.Restart(imposible);
        mainMenu.SetActive(false);
        nextSequence();
    }
}

