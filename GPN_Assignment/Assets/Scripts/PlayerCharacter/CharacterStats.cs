using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxhealth;

    [SerializeField] protected bool isDead;

    private void Start()
    {
        InitVariables();
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
        if (health >= maxhealth)
        {
            health = maxhealth;
        }
    }

    public void Die()
    {
        health = 0;
        isDead = true;
    }

    public void SetHealthTo(int healthToSetTo)
    {
        health = healthToSetTo;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        CheckHealth();
    }

    public void Heal(int heal)
    {
        health += heal;
        CheckHealth();
    }

    public void InitVariables()
    {
        maxhealth = 100;
        SetHealthTo(maxhealth);
        isDead = false;
    }
}
