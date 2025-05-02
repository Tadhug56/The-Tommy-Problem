using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject remyPrefab; // Remy asset prefab (Basic civilian enemey)
    public GameObject gunPowerUpPrefab; // Gun powerup prefab
    public GameObject swatPrefab; // Swat Prefab (Shooter enemey)
    public GameObject gatePrefab; // Gate prefab (Rectangles you run through for stat changes)
    public Transform player;      // Reference to the player to know where to spawn the cubes
    public float spawnInterval; // How often to spawn a cube
    private float lastSpawnTime;

    void Start()
    {
        spawnInterval = 10.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawnTime > (spawnInterval / GroundTile.speed))
        {
            int roll = determineSpawn();

            if(roll <= 60)
            {
                int numberToSpawn = Random.Range(1, 5);

                for(int i = 0; i < numberToSpawn; i++)
                {
                    int spawnChance = Random.Range(1, 10);

                    if(spawnChance <= 5)
                    {
                        SpawnRemy();
                    }

                    else
                    {
                        SpawnSwat();
                    }
                }
            }

            else if(roll > 60)
            {
                SpawnGate();
            }

            lastSpawnTime = Time.time;
        }
    }
    
    private int determineSpawn()
    {
        int roll = Random.Range(1, 100);

        return roll;
    }

    void SpawnRemy()
    {
        float z = Random.Range(-4.5f, 4.5f);
        float spawnHeight = 0.0f;
        float spawnDistance = Random.Range(-50.0f, -40.0f);

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
        float spawnDistance = Random.Range(-50.0f, -40.0f);

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnHeight, z);
        Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject swat = Instantiate(swatPrefab, spawnPosition, spawnRotation);
        swat.transform.SetParent(transform);
        swat.AddComponent<MoveAlongWithGround>();
    }

    void SpawnGate()
    {
        float spawnY = 1.0f;
        float spawnZ = -0.5f;
        float spawnDistance = Random.Range(-50.0f, -40.0f);

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnY, spawnZ);
        
        GameObject gate = Instantiate(gatePrefab, spawnPosition, Quaternion.identity);
        gate.transform.SetParent(transform);
        gate.AddComponent<MoveAlongWithGround>();
    }

    public void ClearGameplay()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
