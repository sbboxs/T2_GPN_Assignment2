using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float dieTime;
    public int damage;
    public GameObject diePEFFECT;
    public Transform player;
    int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
        Physics2D.IgnoreLayerCollision(10, 11);
        Physics2D.IgnoreLayerCollision(9, 10);
        player = GameObject.Find("Player").transform;
        playerHealth = player.GetComponent<PlayerController>().currentHealth;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        Die();
        if (playerHealth > 0 && col.collider.name.Equals(player.GetComponent<PlayerController>().bodyCollider.name))
        {
            player.GetComponent<PlayerController>().TakeDamage(damage);
            Debug.Log("Hit");
        }
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(5);

        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
