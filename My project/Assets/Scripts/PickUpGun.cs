using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// PickUpGun class that handles picking up a gun from a fallen enemy shooter
public class PickUpGun : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // When the player enters the trigger allow them to shoot (Picking up the gun)
    void OnTriggerEnter(Collider other)
    {
        // If collides with player
        if(other.CompareTag("Player"))
        {
            PlayerShooter playerShooter = other.GetComponent<PlayerShooter>(); // Get the PlayerShooter script

            // If playerShooter not set correctly (Should never fail)
            if(playerShooter != null && playerShooter.canShoot != true)
            {
                audioSource.Play(); // Play the equip sound
                playerShooter.canShoot = true; // Allow the player to shoot
            }

            Destroy(gameObject, audioSource.clip.length); // Destroy the object as it has now been picked up (After the audio has been played)
        }
    }
}
