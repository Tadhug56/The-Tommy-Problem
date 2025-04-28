using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Animator animator;
    void OnTriggerEnter(Collider other)
    {       
        if(other.CompareTag("Player"))
        {
            //Debug.Log("Hit Player");
        }
    }
}