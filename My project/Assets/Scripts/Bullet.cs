using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator animator; // Enemy animator used to set "Die" trigger
    private EnemyShooter enemyShooter; // Enemy Shooter script

    private AudioSource audioSource; // Play sound from bullet (Effectively where it hits an enemy)
    public AudioClip hitClip; // Hit an enemy clip

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // When the trigger is entered by an enemy set their "Die" animation trigger and update the kill count
    void OnTriggerEnter(Collider other)
    {
        // If the collision is with an enemy
        if(other.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(hitClip);
            animator = other.GetComponent<Animator>(); // Get the enemies animator
            animator.SetTrigger("Die"); // Set the "Die" trigger to trigger the death animation

            enemyShooter = other.GetComponent<EnemyShooter>(); // Get the Enemy Shooter script from the collided object
            
            // If we could get an EnemyShooter script that means this was an enemy shooter
            if(enemyShooter != null)
            {
                enemyShooter.alive = false; // Set the bool that checks if an enemy is alive (And should be firing) to false
            }

            // Handle updating kills and rewarding extra time
            GameplayUI gameplayUIScript = FindObjectOfType<GameplayUI>();
            gameplayUIScript.remainingTime += 1;
            gameplayUIScript.UpdateKillCountUI();
        }
    }
}
