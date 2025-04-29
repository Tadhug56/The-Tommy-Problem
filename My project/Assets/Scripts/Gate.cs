using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public TextMeshProUGUI gateText;
    public Renderer gateRenderer;
    int roll;
    int increase = 0;

    // Speed variables
    int maxSpeed = 20;
    int minSpeed = 1;

    // Powerup check variables
    bool speedIncrease = false;
    bool healthIncrease = false;

    void Awake()
    {
        gateText.text = generatePowerup();
        GateColour();
    }

    private string generatePowerup()
    {
        roll = Random.Range(1, 101);

        if(roll > 50)
        {
            speedIncrease = true;
            increase = SpeedIncrease();
            return "Speed " + increase;
        }

        else if(roll < 50)
        {
            healthIncrease = true;
            increase = HealthIncrease();
            return "Health : " + increase;
        }

        return "Stat incrase : " + roll;
    }

    private int HealthIncrease()
    {
        int healthIncrease = Random.Range(-5, 5);

        return healthIncrease;
    }

    private int SpeedIncrease()
    {
        int statIncrease = Random.Range(-5, maxSpeed + 1);

        return statIncrease;
    }

    void GateColour()
    {
        if (gateRenderer != null)
        {
            if (increase >= 0)
            {
                gateRenderer.material.color = new Color(0f, 1f, 0f, 0.5f); // Green, 50% transparent
            }

            else
            {
                gateRenderer.material.color = new Color(1f, 0f, 0f, 0.5f); // Red, 50% transparent
            }
        }
    }

    void ApplyStatIncrease()
    {
        if(speedIncrease)
        {
            if(GroundTile.speed + increase > maxSpeed)
            {
                GroundTile.speed = maxSpeed;
                Debug.Log("Max Speed already achieved");
            }

            else if(GroundTile.speed + increase < minSpeed)
            {
                GroundTile.speed = minSpeed;
                Debug.Log("Min Speed already achieved");
            }

            else
            {
                GroundTile.speed += increase;
            }
        }

        else if(healthIncrease)
        {
            Player playerScript = FindObjectOfType<Player>();
            playerScript.health += increase;
            playerScript.UpdateHealthUI();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        { 
            ApplyStatIncrease();
            Debug.Log("Entered gate : " + gateText.text);
        }
    }
}