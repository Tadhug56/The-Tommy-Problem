using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Stat Variables
    public int health;
    private int startingHealth = 3;

    // Condition check Variables
    public bool playable = false; // Checks if the player is in play mode (Move and shoot)
    private bool dead = false; // Checks if the player is dead (Used to differentiate between opening menu and death menu)

    // Position Variables
    public Vector3 startPosition;

    // Reference Variables
    public Animator animator; // Player animator
    private AudioSource audioSource; // Audio Source
    public AudioClip loseClip; // Lose sound
    public TextMeshProUGUI healthUIText; // Health UI

    // Set health at the start of the game
    void Start()
    {
        health = startingHealth;
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Every frame check if we're dead (Health or timer)
    void Update()
    {
        // If health drops to 0 or we died (From timer)
        if(health <= 0 && !dead)
        {
            PlayerLose(); // Handle lose scenario
        }
    }

    // Update the health text
    public void UpdateHealthUI()
    {
        healthUIText.text = "Lives : " + health.ToString();
    }

    // Handle losing logic
    public void PlayerLose()
    {
        audioSource.PlayOneShot(loseClip);

        playable = false; // Turn off moving and shooting
        dead = true; // Set dead
        
        animator.SetTrigger("Lose"); // Play death animation

        GroundTile.speed = 0; // Kill speed (Stop simulating motion)

        FindObjectOfType<CutsceneManager>().StartDeathCutscene(); // Start the death cutscene
    }

    // Reset player and the releveant stats / UI elements
    public void ResetPlayer()
    {
        this.transform.position = startPosition; // Reset to start position

        health = startingHealth; // Reset health to full
        UpdateHealthUI(); // Update health UI

        this.GetComponent<PlayerShooter>().canShoot = false; // Turn off shotting (lost gun)
        
        // If dead and not in opening menu
        if(dead)
        {
            animator.SetTrigger("Reset"); // Reset animation from dead to running
        }

        dead = false; // No longer dead
    }
}
