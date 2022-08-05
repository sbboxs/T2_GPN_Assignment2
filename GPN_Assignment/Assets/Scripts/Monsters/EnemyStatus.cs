using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public Animator enemyAnimator;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Hurt Animation
        enemyAnimator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        //Die animation
        enemyAnimator.SetBool("IsDead", true);
        //Disable the enemy.
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
}
