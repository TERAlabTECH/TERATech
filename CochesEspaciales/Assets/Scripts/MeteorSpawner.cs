using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] GameObject playerShip;

    [SerializeField] float initialSpawnTime = 0.1f;
    [SerializeField] float minimumSpawnTime = 0.001f;

    private float currentSpawnTime;
    private float timePassed = 0;
    private bool banderaTurnOnOff = true;
    void Start()
    {
        currentSpawnTime = initialSpawnTime;
    }

    public void setBandera(bool bandera) //un set para poder prender y apagar los meteoritos
    {
        if (bandera)//si se vuelve a prender, se reinica el valor de currentSpawnTime para q no se vuelva loco el programa
        {
            currentSpawnTime = initialSpawnTime;
        }
        banderaTurnOnOff = bandera;

    }
    void Update()
    {
        if (banderaTurnOnOff) // tal vez haya una forma mas eficiente de hacer este proceso.
        {
            timePassed += Time.deltaTime;

            if (timePassed > currentSpawnTime)
            {
                timePassed = 0;
                SpawnMeteor();

            }
            currentSpawnTime = Mathf.Max(minimumSpawnTime, currentSpawnTime - 0.00005f);
        }
    }
    void SpawnMeteor()
    {
        GameObject instantiatedMeteor = Instantiate(meteorPrefab, spawnPosition, meteorPrefab.transform.rotation);
        instantiatedMeteor.GetComponent<Meteor>().destination = GetRandomPosInBoundingBox();
        instantiatedMeteor.GetComponent<Meteor>().ControlledStart();
        instantiatedMeteor.GetComponent<Meteor>().playerShip = playerShip;
    }
    Vector3 GetRandomPosInBoundingBox()
    {
        float x = Random.Range(-8, 5);
        float y = Random.Range(-94, -85);
        float z = -469.089f;
        Vector3 destination = new Vector3(x, y, z);
        return destination;
    }
}