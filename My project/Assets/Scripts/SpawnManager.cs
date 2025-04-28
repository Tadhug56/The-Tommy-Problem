using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject remyPrefab; // Remy asset prefab (Basic civilian enemey)
    public GameObject gunPowerUpPrefab; // Gun powerup prefab
    public GameObject swatPrefab; // Swat Prefab (Shooter enemey)
    public Transform player;      // Reference to the player to know where to spawn the cubes
    public float spawnInterval = 2f; // How often to spawn a cube
    private float lastSpawnTime;
    private float spawnDistance = -30.0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawnTime > spawnInterval)
        {
            SpawnRemy();
            //SpawnGun();
            SpawnSwat();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnRemy()
    {
        float z = Random.Range(-4.5f, 4.5f);
        float spawnHeight = 0.0f;

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnHeight, z);
        Quaternion spawnRotation = Quaternion.Euler(0f, 270f, 0f);


        GameObject remy = Instantiate(remyPrefab, spawnPosition, spawnRotation);
        remy.transform.SetParent(transform);

        remy.AddComponent<MoveAlongWithGround>();
    }

    public void SpawnGun(float x, float z)
    {
        //float z = Random.Range(-4.5f, 4.5f);
        float spawnHeight = 1f;

        Vector3 spawnPosition = new Vector3(x, spawnHeight, z);

        GameObject powerUp = Instantiate(gunPowerUpPrefab, spawnPosition, Quaternion.identity);
        powerUp.transform.SetParent(transform);
    }

    void SpawnSwat()
    {
        float z = Random.Range(-4.5f, 4.5f);
        float spawnHeight = 0.04f;

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnHeight, z);
        Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject swat = Instantiate(swatPrefab, spawnPosition, spawnRotation);
        swat.transform.SetParent(transform);
        swat.AddComponent<MoveAlongWithGround>();
    }
}
