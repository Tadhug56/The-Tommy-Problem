using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerShooter playerShooter = other.GetComponent<PlayerShooter>();

            if(playerShooter != null)
            {
                playerShooter.canShoot = true;
            }

            Destroy(gameObject);
        }
    }
}
