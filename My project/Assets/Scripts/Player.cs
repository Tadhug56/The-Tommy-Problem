using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI healthUIText;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            playerLose();
        }
    }

    public void UpdateHealthUI()
    {
        healthUIText.text = "Lives : " + health.ToString();
    }

    void playerLose()
    {
        Debug.Log("You lost");
    }
}
