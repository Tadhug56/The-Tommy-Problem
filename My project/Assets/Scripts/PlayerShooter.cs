using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerShooter class that handles player shooting logic
public class PlayerShooter : MonoBehaviour
{
    // Bullet Spawning Variables
    public GameObject bulletPrefab;
    public Transform shootPoint;

    // Shooting Related Variables
    public bool canShoot; // If the player has a gun
    private float shootForce = 20.0f; // // The force of the bullet (Speed effectively)

    // Every frame check if the space bar was pressed to shoot
    void Update()
    {
        // If the space bar was pressed to shoot
        if(canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(); // Shoot
        }
    }

    // Spawn a bulelt and move it forwards with force
    void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity); // Spawn the bullet
        Rigidbody rb = projectile.GetComponent<Rigidbody>(); // Give it a rigidbody so that we can apply force to it and use it for collision checking
        rb.velocity = transform.forward * shootForce; // Add forward velocity

        Destroy(projectile, 3f); // Destroy the bullet after 3 seconds (handles if it doesn't hit anything)
    }
}
