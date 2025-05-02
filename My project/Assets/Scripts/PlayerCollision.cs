using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// PlayerCollision class that handles player collisions (With running over enemies)
public class PlayerCollision : MonoBehaviour
{
    // Reference Variables
    private Animator animator; // Enemy Animator (Used to trigger death animation)
    private Enemy enemy; // Enemy script
    private AudioSource audioSource; // Audio source
    public AudioClip impactClip; // Impact sound

    public GameObject player; // Player object
    private Player playerScript; // Player script

    public float knockbackForce = 5f; // Knockback force when player hits an enemy

    void Start()
    {
        playerScript = player.GetComponent<Player>(); // Get the Player script
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // When colliding with an enemy apply force to them, kill them and update UI
    void OnTriggerEnter(Collider other)
    {
        // If colliding with an enemy
        if(other.CompareTag("Enemy"))
        {
            animator = other.GetComponent<Animator>(); // Get the enemies animator
            animator.SetTrigger("Die"); // Trigger death animation
    
            enemy = other.GetComponent<Enemy>(); // Get the enemy script
            
            ApplyForce(other); // Apply the force of the imapact to the enemy

            audioSource.PlayOneShot(impactClip);

            // if enemy script not set correctly (Should never fail)
            if(enemy != null)
            {
                enemy.alive = false; // Set alive to false (Stops shooting for enemyShooter)

                GameplayUI gameplayUIScript = FindObjectOfType<GameplayUI>(); // Get the GameplayUI script (Can only get it when the gameplay is on)
                gameplayUIScript.remainingTime += 1; // Add the rewarded extra time
                gameplayUIScript.UpdateKillCountUI(); // Update the UI
            }
            
        }
    }

    // Applies force to the collided enemy
    private void ApplyForce(Collider other)
    {
        // Apply physics knockback

        Rigidbody rb = other.GetComponent<Rigidbody>(); // Get the rigidbody

        // If the rigidbody was selected and the enemy is alive (Prevents multiple knockbakcs)
        if (rb != null && enemy.alive)
        {
            Vector3 dir = (other.transform.position - transform.position).normalized; // Direction from player to enemy (Chatgpt solution)
            
            dir.y = 0; // Zero out any vertical so they fly straight back horizontally:
        
            rb.AddForce(dir * knockbackForce, ForceMode.Impulse); // Apply the impulse
        }
    }
}