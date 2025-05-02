using UnityEngine;

// EnemyBullet class to handle enemybullet specific logic
public class EnemyBullet : MonoBehaviour
{   
    // If the bullet hits the player they lose a health point
    void OnTriggerEnter(Collider other)
    {       
        // If the collider is a player
        if(other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>(); // Get the required script
            playerScript.health -= 1; // Subtract the health
            playerScript.UpdateHealthUI(); // Update the UI with the new value
        }
    }
}