using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] GameObject explosion; 
    void OnTriggerEnter(Collider other){
        Debug.Log("asteroidHit");
        // ContctPoint contact= other.contacts[0];
        Instantiate(explosion, other.transform.position, Quaternion.identity);
    }
}
