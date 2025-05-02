using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GroundTile class handles the moving planes that simulate motion
public class GroundTile : MonoBehaviour
{

    // Game pace related variables
    public static float startingSpeed = 5.0f; // Starting speed value
    public static float speed; // The speed at which the tiles will move forwards
    public float tileLength = 30.0f; // The lenth of each tile
    private float distanceTraveled = 0f; // How far a tile has travelled (Used to track if tile should be reset)

    // Player related variables
    public Transform player; // Player's Transform

    // Set the speed at the start of the game (Specifically needed for first run of the game)
    void Start()
    {
        speed = startingSpeed; // Set the speed
    }

    // Move the tile every frame based on speed and reset it if it's too far past
    void Update()
    {
        float moveAmount = speed * Time.deltaTime; // Move speed relevant to frame time

        transform.position += new Vector3(moveAmount, 0, 0); // Move the tile forward across the x access

        distanceTraveled += moveAmount; // Add the distance travelled

        // If the tile moves its entire length accross the x access move it to the end of the queue
        if(distanceTraveled > tileLength)
        {
            transform.position -= new Vector3(tileLength, 0, 0); // Set the tile back based on its length
            distanceTraveled -= tileLength; // Reset the distance travelled
        }
    }
}
