using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    private float shootForce = 20.0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if(alive)
        {
            GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 power = transform.forward * shootForce;
            Vector3 groundAdjustment = Vector3.right * GroundTile.speed;
            rb.velocity = power + groundAdjustment;

            // Optional: auto-destroy after a few seconds
            Destroy(projectile, 3f);
        }
    }
}