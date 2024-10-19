using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] AudioSource music1;
    [SerializeField] AudioSource music2;
    [SerializeField] AudioSource music3;
    [SerializeField] AudioSource music4;

    public void onMusic1() 
    {  
        music1.Play(); 
    }
    public void onMusic2()
    {
        music2.Play();
    }
    public void onMusic3()
    {
        music3.Play();
    }
    public void onMusic4()
    {
        music4.Play();
    }
}
