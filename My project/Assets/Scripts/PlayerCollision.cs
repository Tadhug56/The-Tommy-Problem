using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCollision : MonoBehaviour
{
    private Animator animator;
    private Enemy enemy;

    public GameObject player;
    private Player playerScript;

    public float knockbackForce = 5f;

    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("EnemyHit");
            animator = other.GetComponent<Animator>();
            animator.SetTrigger("Die");
    
            enemy = other.GetComponent<Enemy>();
            
            // apply physics knockback
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null && enemy.alive)
            {
                // direction from player → enemy
                Vector3 dir = (other.transform.position - transform.position).normalized;
                // zero out any vertical so they fly straight back horizontally:
                dir.y = 0;
                // apply impulse
                rb.AddForce(dir * knockbackForce, ForceMode.Impulse);
            }

            if(enemy != null)
            {
                enemy.alive = false;
            }
        }

        else if(other.CompareTag("EnemyBullet"))
        {
            playerScript.health -= 1;
        }
    }
}