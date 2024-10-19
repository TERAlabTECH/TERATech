using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


//Este script maneja la secuencia aleatoria que el usuario debe de seguir
public class Secuencia : MonoBehaviour
{
    // Start is called before the first frame update
    LevelManager manager;
    private List<GameObject> luces; 
    public List<int> currentSequence;

    public UnityEvent playSequence; 
    public int sequenceLenght=1;

    //Boleano para saber si se esta tocando la secuencia
    public bool playingSequence;

    public bool imposible=false;
    public void Start()
    {       
        sequenceLenght=1;
        luces=GetComponent<LevelManager>().luces;
        playSequence.AddListener(()=>StartCoroutine(playSequenceroutine()));
    }


    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space)){
            triggerSequence();
        }
        
    }
    public void triggerSequence(){
        playSequence.Invoke();
    }    

    IEnumerator playSequenceroutine(){

        playingSequence=true;

        List<int> drumSequence= GenerateSequence(sequenceLenght, imposible);
        for(int i=0; i<sequenceLenght; i++){
            luces[drumSequence[i]].GetComponent<Sphere>().triggerSphere();
            yield return new WaitForSeconds(0.6f);
        }
        
        playingSequence=false;
        sequenceLenght+=1;
        
        yield return null;

    }
    public List<int> GenerateSequence(int size, bool imposible){
        if(imposible){

            List<int> drumSequenceList=new List<int>();



            for(int i =0; i<size; i++){
                drumSequenceList.Add(UnityEngine.Random.Range(0,4));
            }


            currentSequence=drumSequenceList;

        }else{
            currentSequence.Add(UnityEngine.Random.Range(0,4));
        }

        return currentSequence;

    }
    public void Restart(bool imposibleVar){
        currentSequence.Clear();
        sequenceLenght=1;
        imposible=imposibleVar;
        
    }
    
    
}
