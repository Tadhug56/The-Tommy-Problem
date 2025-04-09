using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab; // The cube prefab to spawn
    public Transform player;      // Reference to the player to know where to spawn the cubes
    public float spawnInterval = 2f; // How often to spawn a cube
    private float lastSpawnTime;

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawnTime > spawnInterval)
        {
            SpawnRemy();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnRemy()
    {
        float spawnDistance = -30.0f;
        float spawnHeight = 0.0f;
        float z = Random.Range(-4.5f, 4.5f);

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnHeight, z);
        Quaternion spawnRotation = Quaternion.Euler(0f, 270f, 0f);


        GameObject remy = Instantiate(prefab, spawnPosition, spawnRotation);
        remy.transform.SetParent(transform);

        remy.AddComponent<MoveAlongWithGround>();
    }
}
