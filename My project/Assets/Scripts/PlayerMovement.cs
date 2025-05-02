using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerMovement Script
public class PlayerMovement : MonoBehaviour
{
    // Stat Variables
    private float speed = 5.0f; // Movespeed
    private float moveRange = 4.5f; // Move range (Stay in the straight)

    // Input variables
    private float input; // Input

    // Transform Variables
    private Vector3 startPosition; // The start position of the player

    // Reference Variables
    private Player playerScript; // Player script

    // Set the start position and get the Player script
    void Start()
    {
        startPosition = transform.position; //Set the start position
        playerScript = FindObjectOfType<Player>(); // Get the Player script
    }

    // Every frame take in the input
    void Update()
    {
        // If not in a cutscene / menu
        if(playerScript.playable)
        {
            input = Input.GetAxis("Horizontal"); // Get horizontal input (A - D)

            Vector3 newPosition = transform.position + new Vector3(0f, 0f, input * speed * Time.deltaTime); // Calculate the new position based on input

            newPosition.z = Mathf.Clamp(newPosition.z, startPosition.z - moveRange, startPosition.z + moveRange); // Clamp it so that it's smooth (Chatgpt solution)

            transform.position = newPosition; // Set the new position
        }
    }
}
