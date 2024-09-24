using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject aguaEffect;
    void Start()
    {
        aguaEffect.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void OnApplicationQuit(){
        aguaEffect.SetActive(false);

    }
}
