using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void abreMenu(){
        SceneManager.LoadScene("Menu");

    }
    public void abreNave(){
        SceneManager.LoadScene("GyroscopeVersion");
    }

    public void abreAR()
    {
        SceneManager.LoadScene("AR");
    }
}
