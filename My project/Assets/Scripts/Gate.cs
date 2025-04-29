using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public TextMeshProUGUI gateText;
    int roll;
    float increase = 0;

    // Speed variables
    float maxSpeed = 20.0f;
    float minSpeed = 1.0f;

    // Powerup check variables
    bool speedIncrease = false;
    void Awake()
    {
        gateText.text = generatePowerup();
    }

    private string generatePowerup()
    {
        roll = Random.Range(1, 101);

        if(roll > 30)
        {
            speedIncrease = true;
            increase = SpeedIncrease();
            return "Speed " + increase;
        }

        return "Stat incrase : " + roll;
    }

    private float SpeedIncrease()
    {
        float statIncrease = Random.Range(-5, maxSpeed + 1);

        return statIncrease;
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