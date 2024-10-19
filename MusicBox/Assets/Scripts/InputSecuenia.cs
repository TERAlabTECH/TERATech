using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

//Este script maneja el input del usuario que debe de copiar la sequencia que se toco. 
public class InputSecuenia : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> luces;
    private bool playingSequence;
    private LevelManager manager;
    private Secuencia sequence;

    public List<int> sequencePlayed;
    private List<int> currentSequence;
    void Start()
    {

        manager=GetComponent<LevelManager>();
        luces=manager.luces;
        sequence=GetComponent<Secuencia>();
        playingSequence=sequence.playingSequence;
        currentSequence=sequence.currentSequence;
        
    }

    public void playDrum(int drumId){
        playingSequence=sequence.playingSequence;
        currentSequence=sequence.currentSequence;
        currentSequence.ToString();

        //Check if a sequence is being played.
        if(!playingSequence && sequencePlayed.Count<currentSequence.Count){
            
            luces[drumId].GetComponent<Sphere>().triggerSphere();
            sequencePlayed.Add(drumId);
            if(!isInputCorrect()){
                manager.gameOver();
                return;
            }
        }
        if(!(sequencePlayed.Count<currentSequence.Count)){
            sequencePlayed.Clear();
            manager.nextSequence();
        }
    }

    //checks if sequence was played correctly
    bool isInputCorrect(){
        int currentCount= sequencePlayed.Count-1;
        if(sequencePlayed[currentCount]==currentSequence[currentCount]){
            return true;
        }else{
            return false;
        }

    }
    public void Restart(){
        sequencePlayed.Clear();
        currentSequence.Clear();
    }   

}
