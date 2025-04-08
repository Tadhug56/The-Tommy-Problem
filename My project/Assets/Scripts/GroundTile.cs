using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{

    // Game pace related variables
    public float speed = 5.0f; // The speed at which the tiles will move forwards
    public float tileLength = 30.0f; // The lenth of each tile
    private Vector3 startPosition = new Vector3(-60 ,0, 0); // The start position of the last tile in the queue

    // Player related variables
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0); // Move the tile forward across the x access

        // If the tile moves its entire length accross the x access move it to the end of the queue
        if(transform.position.x > tileLength)
        {
            transform.position = startPosition;
        }
    }
}
