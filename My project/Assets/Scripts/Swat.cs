using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swat : MonoBehaviour
{
    public GameObject gunPowerUp;

    private SpawnManager spawnManager;

    void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet") || other.CompareTag("Player"))
        {
            spawnManager.SpawnGun(this.transform.position.x, this.transform.position.z);
        }
        
    }
}
