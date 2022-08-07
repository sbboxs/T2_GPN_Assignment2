using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float dieTime;
    public int damage;
    public GameObject diePEFFECT;
    public Transform monster;
    int bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 13);
        monster = GameObject.Find("Monster").transform;
        bossHealth = monster.GetComponent<Boss>().currentHealth;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        Die();
        if (bossHealth > 0 && col.collider.name.Equals(monster.GetComponent<Boss>().bodyCollider.name))
        {
            monster.GetComponent<Boss>().TakeDamage(damage);
            Debug.Log("Hit");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
