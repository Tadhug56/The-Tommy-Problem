using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator animator;
    EnemyShooter enemyShooter;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            animator = other.GetComponent<Animator>();
            animator.SetTrigger("Die");

            enemyShooter = other.GetComponent<EnemyShooter>();
            if(enemyShooter != null)
            {
                enemyShooter.alive = false;
                Timer.remainingTime += 2;
            }
        }
    }
}
