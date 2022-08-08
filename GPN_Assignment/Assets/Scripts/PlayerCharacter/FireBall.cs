using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float dieTime;
    public int damage;
    public GameObject diePEFFECT;
    GameObject[] skeleton;
    GameObject[] archer;
    GameObject boss;
    int bossHealth;
    int skeletonHealth;
    int archerHealth;

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
        foreach (GameObject monster1 in skeleton)
        {
            skeletonHealth = monster1.GetComponent<Skeleton>().currentHealth;
            if (skeletonHealth > 0 && col.collider.name.Equals(monster1.GetComponent<Skeleton>().bodyCollider.name))
            {
                monster1.GetComponent<Skeleton>().TakeDamage(damage);
                Debug.Log("Hit");
            }
        }

        foreach (GameObject monster1 in archer)
        {
            archerHealth = monster1.GetComponent<Archer>().currentHealth;
            if (archerHealth > 0 && col.collider.name.Equals(monster1.GetComponent<Archer>().bodyCollider.name))
            {
                monster1.GetComponent<Archer>().TakeDamage(damage);
                Debug.Log("Hit");
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
