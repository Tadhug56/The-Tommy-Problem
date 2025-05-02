using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyShooter class that inherits from Enemy, handles firing bullets
public class EnemyShooter : Enemy
{
    public GameObject bulletPrefab; // Bullet prefab
    public Transform shootPoint; // Where the bullet spawns
    public static float fireRate; // How often to fire
    private float nextFireTime = 0f; // Timing variable
    private float shootForce = 20.0f; // How fast (How much force) to shoot the bullet

    // Every frame check if we should shoot another bullet
    void Update()
    {
        // If we should shoot
        if (Time.time >= nextFireTime)
        {
            Shoot(); // Shoot the bullet
            nextFireTime = Time.time + 1f / fireRate; // Reset the next fire time
        }
    }

    // Reset the firerate potentially changed by the gates
    public void ResetFirerate()
    {
        fireRate = 0.2f; // Original rate
    }

    // If the enemy shooter is alive shoot (Instantiate) a bullet at a position from their gun
    void Shoot()
    {
        // If alive
        if(alive)
        {
            GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation); // Instantiate the bullet
            
            // Handle the force of the bullet
            Rigidbody rb = projectile.GetComponent<Rigidbody>(); // Get the rigidbody
            Vector3 power = transform.forward * shootForce; // Simple power
            Vector3 groundAdjustment = Vector3.right * GroundTile.speed; // Adjust for ground speed
            rb.velocity = power + groundAdjustment; // Add the velocity

            // Destroy the bullet after 3 seconds so as not to infinetly fly (Would get handled by the destroy logic behind the player)
            Destroy(projectile, 3f);
        }
    }
}