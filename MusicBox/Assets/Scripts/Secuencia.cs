using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Secuencia : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> luces; 
    public Vector3 position1;
    [Range(0,1)]public float alphaTest=1;
    Renderer luz1; 
    public float duration=2f;
    public List<int> currentSequence;

    public UnityEvent playSequence; 
    private int sequenceLenght=1;
    void Start()
    {
        playSequence.AddListener(()=>StartCoroutine(playSequenceroutine()));
        
    }


    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space)){
            triggerSequence();
        }
        
    }
    public List<int> GenerateSequence(int size){
        List<int> drumSequenceList=new List<int>();



        for(int i =0; i<size; i++){
            drumSequenceList.Add(UnityEngine.Random.Range(0,4));
        }

        currentSequence=drumSequenceList;
        return drumSequenceList;

    }
    public void triggerSequence(){
        playSequence.Invoke();
    }    

    IEnumerator playSequenceroutine(){

        List<int> drumSequence= GenerateSequence(sequenceLenght);
        for(int i=0; i<sequenceLenght; i++){
            luces[drumSequence[i]].GetComponent<Sphere>().triggerSphere();
            yield return new WaitForSeconds(0.6f);
        }
        
        sequenceLenght+=1;
        yield return null;

    }
    
    


}
