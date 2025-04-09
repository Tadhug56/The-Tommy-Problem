using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform shootPoint;

    public bool canShoot;
    private float shootForce = 20.0f;

    // Update is called once per frame
    void Update()
    {
        if(canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * shootForce;

        // Optional: auto-destroy after a few seconds
        Destroy(projectile, 3f);
    }
}
