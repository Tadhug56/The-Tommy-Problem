using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn manager class that handles spawning of gates and enemies
public class SpawnManager : MonoBehaviour
{
    public GameObject remyPrefab; // Remy asset prefab (Basic civilian enemey)
    public GameObject gunPowerUpPrefab; // Gun powerup prefab
    public GameObject swatPrefab; // Swat Prefab (Shooter enemey)
    public GameObject gatePrefab; // Gate prefab (Rectangles you run through for stat changes)
    public Transform player;      // Reference to the player to know where to spawn the cubes
    public float spawnInterval; // How often to spawn a cube
    private float lastSpawnTime;

    void Awake()
    {
        spawnInterval = 10.0f; // Set the spawn interval whenever the spawn manager is turned on (Set to 10 as starting GroundTile.speed is = 5 (10 / 5 = 2))
    }

    // Update is called once per frame
    void Update()
    {
        // If the timing of spawn interval has been passed (Divided by GroundTile.speed to reflect the simulated increase and decrease in movements)
        if(Time.time - lastSpawnTime > (spawnInterval / GroundTile.speed))
        {
            int roll = determineSpawn(); // Roll a random number to determine if a gate or enemies should spawn

            // 80% chance
            if(roll <= 80)
            {
                int numberToSpawn = Random.Range(1, 5); // Roll a number to determine how many enemies should spawn in this batch (Creates variance among batches)

                // Spawn enemies based on the generated numberToSpawn
                for(int i = 0; i < numberToSpawn; i++)
                {
                    int spawnChance = Random.Range(1, 10); // Roll a random number to determine which enemy to spawn

                    // Spawn default npc 50%
                    if(spawnChance <= 5)
                    {
                        SpawnRemy();
                    }

                    // Spawn shooter enemy 50%
                    else
                    {
                        SpawnSwat();
                    }
                }
            }

            // 20% chance
            else if(roll > 80)
            {
                SpawnGate(); // Spawn a gate
            }

            lastSpawnTime = Time.time; // Set the last spawn time
        }
    }
    
    // Roll a random number to determine spawn range on the x axis
    private int determineSpawn()
    {
        int roll = Random.Range(1, 100); // Roll between 1 and 100

        return roll; // Return the rolled value
    }

    // Spawn a default npc (Model Remy)
    void SpawnRemy()
    {
        float z = Random.Range(-4.5f, 4.5f); // Random range within the bounds of the road
        float spawnHeight = 0.0f; // Height at which the model should be loaded to spawn accurately
        float spawnDistance = Random.Range(-60.0f, -40.0f); // Random range to spawn the npc at

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnHeight, z); // Variable holding spawn position
        Quaternion spawnRotation = Quaternion.Euler(0f, 270f, 0f); // Rotation that the model needs to be spawned accurately


        GameObject remy = Instantiate(remyPrefab, spawnPosition, spawnRotation); // Spawn the npc at the set position and rotation
        remy.transform.SetParent(transform); // Make it a child of SpawnManager

        remy.AddComponent<MoveAlongWithGround>(); // Give it the MoveAlongWithGround script that simulates it's motion
    }

    // Spawn a gun (Called when killing an EnemyShooter)
    public void SpawnGun(float x, float z)
    {
        //float z = Random.Range(-4.5f, 4.5f); // Was used when spawning the powerup generally (Might keep incase I want to change it back)
        float spawnHeight = 1f; // Height for the powerup to spawn so that it correctly collides with the player as expected

        Vector3 spawnPosition = new Vector3(x, spawnHeight, z); // Variable to store spawn posiiton

        GameObject powerUp = Instantiate(gunPowerUpPrefab, spawnPosition, Quaternion.identity); // Spawn the powerup at the set position and its default rotation
        powerUp.transform.SetParent(transform); // Make it a child of SpawnManager
    }

    // Spawn an EnemyShooter
    void SpawnSwat()
    {
        float z = Random.Range(-4.5f, 4.5f); // Random range within the bounds of the road
        float spawnHeight = 0.04f; // Height at which the model should be loaded to spawn accurately
        float spawnDistance = Random.Range(-50.0f, -40.0f); // Random range to spawn the npc at

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnHeight, z); // Variable holding spawn position
        Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f); // Rotation that the model needs to be spawned accurately

        GameObject swat = Instantiate(swatPrefab, spawnPosition, spawnRotation); // Spawn the npc at the set position and rotation
        swat.transform.SetParent(transform);  // Make it a child of SpawnManager
        swat.AddComponent<MoveAlongWithGround>(); // Give it the MoveAlongWithGround script that simulates it's motion
    }

    // Spawn a gate that gives the player or enemies buffs / defuffs when touched
    void SpawnGate()
    {
        float spawnY = 1.0f; // Spawn height for the gate to correctly be placed
        float spawnZ = -0.5f; // Spawn range for each gate to have half the road
        float spawnDistance = Random.Range(-50.0f, -40.0f); // Random range to spawn it between to give variance between spawn's of gates and the distances between them when back to back

        Vector3 spawnPosition = new Vector3(spawnDistance, spawnY, spawnZ); // Variable to store the spawn position
        
        GameObject gate = Instantiate(gatePrefab, spawnPosition, Quaternion.identity); // Spawn the gate at the set position and default rotation
        gate.transform.SetParent(transform); // Make it a child of SpawnManager
        gate.AddComponent<MoveAlongWithGround>(); // Give it the MoveAlongWithGround script that simulates it's motion
    }

    // Clear / delete all children of the SpawnManager (Should always include everything Instantiated)
    public void ClearGameplay()
    {
        // For each child remove (Is there a cleaner way to do this?)
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject); // Destroy the GameObject
        }
    }
}
