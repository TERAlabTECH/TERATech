using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject meteorPrefab; 
    [SerializeField] Vector3 spawnPosition; 
    [SerializeField] GameObject playerShip; 
    private float timePassed=0; 
    void Start()
    {
        
    }

    void Update()
    {
        timePassed+=Time.deltaTime;

        if(timePassed>0.1){
            timePassed=0; 
            SpawnMeteor();
        }
    }
    void SpawnMeteor(){
        GameObject instantiatedMeteor= Instantiate(meteorPrefab, spawnPosition, meteorPrefab.transform.rotation);
        instantiatedMeteor.GetComponent<Meteor>().destination=GetRandomPosInBoundingBox();
        instantiatedMeteor.GetComponent<Meteor>().ControlledStart();
        instantiatedMeteor.GetComponent<Meteor>().playerShip=playerShip;
    }
    Vector3 GetRandomPosInBoundingBox(){
        float x= Random.Range(-12,9); 
        float y= Random.Range(-99, -80);
        float z= -469.089f;
        Vector3 destination= new Vector3(x,y,z);
        return destination;
    }
}
