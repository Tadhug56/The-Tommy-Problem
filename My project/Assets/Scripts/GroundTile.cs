using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{

    // Game pace related variables
    public static float speed = 5.0f; // The speed at which the tiles will move forwards
    public float tileLength = 30.0f; // The lenth of each tile
    private float distanceTraveled = 0f;

    // Player related variables
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        float moveAmount = speed * Time.deltaTime;

        transform.position += new Vector3(moveAmount, 0, 0); // Move the tile forward across the x access

        distanceTraveled += moveAmount;

        // If the tile moves its entire length accross the x access move it to the end of the queue
        if(distanceTraveled > tileLength)
        {
            transform.position -= new Vector3(tileLength, 0, 0);
            distanceTraveled -= tileLength;
        }
    }
}
