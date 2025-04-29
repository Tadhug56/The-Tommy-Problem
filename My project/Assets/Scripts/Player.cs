using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    private int startingHealth = 3;
    public bool playable = false;
    private bool dead = false;

    public Vector3 startPosition;

    public Animator animator;
    public TextMeshProUGUI healthUIText;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0 && !dead)
        {
            PlayerLose();
        }
    }

    public void UpdateHealthUI()
    {
        healthUIText.text = "Lives : " + health.ToString();
    }

    public void PlayerLose()
    {
        Debug.Log("You lost");
        playable = false;
        dead = true;
        
        animator.SetTrigger("Lose");
        GroundTile.speed = 0;
        FindObjectOfType<CutsceneManager>().StartDeathCutscene();
    }

    public void ResetPlayer()
    {
        this.transform.position = startPosition;
        health = startingHealth;
        UpdateHealthUI();
        
        if(dead)
        {
            animator.SetTrigger("Reset");
        }

        dead = false;
    }
}
