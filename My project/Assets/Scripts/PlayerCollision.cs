using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCollision : MonoBehaviour
{
    Animator animator;
    EnemyShooter enemyShooter;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("EnemyHit");
            animator = other.GetComponent<Animator>();
            animator.SetTrigger("Die");
    
            enemyShooter = other.GetComponent<EnemyShooter>();
            if(enemyShooter != null)
            {
                enemyShooter.alive = false;
            }
           
        }
    }
}