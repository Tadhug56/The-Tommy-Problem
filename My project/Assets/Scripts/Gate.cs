using TMPro;
using UnityEngine;

// Gate class that handles buffs and debuffs to the player and enemies
public class Gate : MonoBehaviour
{
    // Gate variables
    public TextMeshProUGUI gateText; // Text on the gate
    public Renderer gateRenderer; // Gate Renderer
    private AudioSource audioSource; // Audio source
    public AudioClip positiveClip; // Positive sound
    public AudioClip negativeClip; // Negative sound
    int roll; // Roll for which buff to give the gate
    int increase = 0; // The change in the stat value

    // Speed variables (Min and max for bounding)
    int maxSpeed = 20;
    int minSpeed = 1;

    // Enemy firerate variables (Min and max for bounding)
    float minFirerate = 0.1f;
    float maxFirerate = 3.0f;

    // Powerup check variables (Used to determine which powerup was selected)
    bool speedIncrease = false;
    bool healthIncrease = false;
    bool enemyFirerateIncrease = false;
    bool timerIncrease = false;

    // When spawned generate the powerup and determine the colour based on the increase value
    void Awake()
    {
        gateText.text = generatePowerup(); // Set the text to the value determined in the roll
        GateColour(); // Set the colour of the gate based on if the increase is a + or -
        
        audioSource = GetComponent<AudioSource>();
    }

    // Generate which buff / powerup based on a dice roll
    private string generatePowerup()
    {
        roll = Random.Range(1, 101); // Odds 1 - 101 

        // Speed increase ~20% odds
        if(roll < 20)
        {
            speedIncrease = true;
            increase = statIncrease(-5, maxSpeed + 1);
            return "Speed " + increase;
        }

        // Health increase ~20% odds
        else if(roll >= 20 && roll < 40)
        {
            healthIncrease = true;
            increase = statIncrease(-5, 5);
            return "Health : " + increase;
        }

        // Time increase ~20% odds
        else if(roll >= 40 && roll < 80)
        {
            timerIncrease = true;
            increase = statIncrease(-30, 30);
            return "Timer : " + increase;
        }

        // Enemy firerate increase ~20% odds
        else if(roll >= 80)
        {
            enemyFirerateIncrease = true;
            increase = statIncrease(-1, 1);
            return "Enemy Firerate : " + increase;
        }

        // Nothing ~1% odds
        return "Redemption"; // Empty gate joke (There is no redemption after what Tommy has done (And no stat increases either)) 
    }

    // General statIncrease method that returns the value based on an upper and lower bound provided as parameters
    private int statIncrease(int lowerBound, int upperBound)
    {
        int statIncrease = Random.Range(lowerBound, upperBound);

        return statIncrease;
    }

    // Set the colour of the gate based on if the increase is a + or -
    void GateColour()
    {
        // If the gate renderer was set (Should never fail if only used on a gate prefab)
        if (gateRenderer != null)
        {
            // If increase is positve set green
            if (increase >= 0)
            {
                gateRenderer.material.color = new Color(0f, 1f, 0f, 0.5f); // Green, 50% transparent
            }

            // Else increase is negative set red
            else
            {
                gateRenderer.material.color = new Color(1f, 0f, 0f, 0.5f); // Red, 50% transparent
            }
        }
    }

    // Actually applies the stat increase to the stat's related value
    void ApplyStatIncrease()
    {
        if(increase >= 0)
        {
            audioSource.PlayOneShot(positiveClip);
        }

        else
        {
            audioSource.PlayOneShot(negativeClip);
        }
        // If speed increase
        if(speedIncrease)
        {
            // Max speed upper bound
            if(GroundTile.speed + increase > maxSpeed)
            {
                GroundTile.speed = maxSpeed;
            }

            // Min speed lower bound
            else if(GroundTile.speed + increase < minSpeed)
            {
                GroundTile.speed = minSpeed;
            }

            // Else change the speed as required
            else
            {
                GroundTile.speed += increase;
            }
        }

        // If health increase
        else if(healthIncrease)
        {
            Player playerScript = FindObjectOfType<Player>(); // Get Player script
            playerScript.health += increase; // Increase the health variable
            playerScript.UpdateHealthUI(); // Update the UI
        }

        // If timer increase
        else if(timerIncrease)
        {
            GameplayUI gameplayUIScript = FindObjectOfType<GameplayUI>(); // Get GameplayUI script
            gameplayUIScript.remainingTime += increase; // Increase the timer variable
            gameplayUIScript.UpdateTimerUI(); // Update the UI
        }

        // If enemy firerate increase
        else if(enemyFirerateIncrease)
        {
            // Max firerate upper bound
            if(EnemyShooter.fireRate + increase >= maxFirerate)
            {
                EnemyShooter.fireRate = maxFirerate;
            }

            // Min firerate lower bound
            else if(EnemyShooter.fireRate + increase <= minFirerate)
            {
                EnemyShooter.fireRate = minFirerate;
            }

            // Else change the value as required
            else
            {
                EnemyShooter.fireRate += increase;
            }
        }
    }

    // When a player enters a gate apply the stat increase
    void OnTriggerEnter(Collider other)
    {
        // If player collides
        if(other.CompareTag("Player"))
        { 
            ApplyStatIncrease(); // Apply the stat increase
        }
    }
}