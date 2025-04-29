using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Animator animator;
    void OnTriggerEnter(Collider other)
    {       
        if(other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            playerScript.health -= 1;
            playerScript.UpdateHealthUI();
        
        }
    }
}