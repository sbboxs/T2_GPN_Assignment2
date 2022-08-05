using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed, range/*, timeBTWShots, shootSpeed*/;
    private float distToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;
    private bool mustTurn2;
    int playerHealth;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Transform objectCheckPos;
    public LayerMask groundLayer;
    public LayerMask obstacles;
    public LayerMask enemy;
    public LayerMask players;
    public Collider2D bodyCollider;
    public Transform player;
    private Collider2D[] enemies;
    public Transform shootPos;
    public Collider2D playerCollider;

    // Variable for monster status
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public int maxHealth = 100;
    int currentHealth;
    int atkDMG = 10;

    public Animator skeletonAnimator;
    //public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
    }

    // Update is called per frame
    void Update()
    {
        skeletonAnimator.SetBool("IsRunning", mustPatrol);
        playerHealth = playerCollider.GetComponent<PlayerController>().currentHealth;

        if (mustPatrol)
        {
            Patrol();
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= range && currentHealth > 0 && playerHealth > 0)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            if (Physics2D.OverlapCircle(shootPos.position, 0.1f, players) && Time.timeSinceLevelLoad >= nextAttackTime)
            {
                mustPatrol = false;
                rb.velocity = Vector2.zero;
                nextAttackTime = Time.timeSinceLevelLoad + attackRate;
                skeletonAnimator.SetTrigger("Attack");
                StartCoroutine(WaitForAnimation());
            }

            //StartCoroutine(Shoot());
        }
        else if (currentHealth <= 0 || playerHealth <= 0)
        {
            mustPatrol = false;
            rb.velocity = Vector2.zero;
        }
        else
        {
            mustPatrol = true;
        }
    }

    void FixedUpdate()
    {
        if (mustPatrol)
        {
            //true
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        enemies = Physics2D.OverlapCircleAll(objectCheckPos.position, 0.1f, enemy);

        if (mustTurn || bodyCollider.IsTouchingLayers(obstacles))
        {
            Flip();
        }

        // Allow monster to walk past each other
        if (enemies.Length != 0)
        {
            if (bodyCollider.IsTouching(enemies[0]))
            {
                Physics2D.IgnoreCollision(bodyCollider, enemies[0]);
            }
        }

        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Hurt Animation
        skeletonAnimator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Die animation
        skeletonAnimator.SetBool("IsDead", true);
        //Disable the enemy.
        GetComponent<Collider2D>().enabled = false;
        // Heals player
        playerCollider.GetComponent<PlayerController>().currentHealth += 20;
        // Player gains exp
        playerCollider.GetComponent<PlayerController>().exp += 20;
        // Monster revive
        StartCoroutine(MonsterRespawn());
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1);

        playerCollider.GetComponent<PlayerController>().TakeDamage(atkDMG);
    }

    IEnumerator MonsterRespawn()
    {
        yield return new WaitForSeconds(10);

        skeletonAnimator.SetBool("IsDead", false);
        GetComponent<Collider2D>().enabled = true;
        currentHealth = maxHealth;
        mustPatrol = true;
    }

    //IEnumerator Shoot()
    //{
    //    yield return new WaitForSeconds(timeBTWShots);
    //    GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

    //    newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);
    //}
}
