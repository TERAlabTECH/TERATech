using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 2.0f;    // Speed of the rotation
    public float radius = 1.0f;   // Radius of the circle
    public float height = 0.5f;   // Height of the up-and-down movement

    private Vector3 originalPosition;
    float randomFactor=0;
    void Start()
    {
        originalPosition = transform.position;
        float randomFactor= UnityEngine.Random.Range(-30f,30f);
    }

    void Update()
    {
        // // Add randomization to the radius using Perlin noise
        // float randomRadius = Mathf.PerlinNoise(0, Time.time*randomFactor) * 0.5f;

        // // Calculate the new position in the XZ plane (horizontal circle)
        // float newX = Mathf.Cos(Time.time * speed) * randomRadius;
        // float newZ = Mathf.Sin(Time.time * speed) * randomRadius;

        // // For Y-axis movement, we add independent oscillation using sine
        // float newY = Mathf.Sin(Time.time * speed) * randomRadius;

        // // Update the object's position with both circular and vertical movement
        // transform.position = new Vector3(originalPosition.x + newX, originalPosition.y + newY, originalPosition.z + newZ);
    }
}
