using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float dieTime;
    public int damage;
    public int manaCost;
    public GameObject diePEFFECT;
    GameObject[] skeleton;
    GameObject[] archer;
    GameObject boss;
    int bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 13);
        skeleton = GameObject.FindGameObjectsWithTag("Skeleton");
        archer = GameObject.FindGameObjectsWithTag("Archer");
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossHealth = boss.GetComponent<Boss>().currentHealth;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        Die();
        foreach (GameObject skele in skeleton)
        {
            int health = skele.GetComponent<Skeleton>().currentHealth;
            if (health > 0 && col.collider.name.Equals(skele.GetComponent<Skeleton>().bodyCollider.name))
            {
                skele.GetComponent<Skeleton>().TakeDamage(damage);
            }
        }
        foreach (GameObject arc in archer)
        {
            int health = arc.GetComponent<Archer>().currentHealth;
            if (health > 0 && col.collider.name.Equals(arc.GetComponent<Archer>().bodyCollider.name))
            {
                arc.GetComponent<Skeleton>().TakeDamage(damage);
            }
        }
        if (bossHealth > 0 && col.collider.name.Equals(boss.GetComponent<Boss>().bodyCollider.name))
        {
            boss.GetComponent<Boss>().TakeDamage(damage);
            Debug.Log("Hit");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
