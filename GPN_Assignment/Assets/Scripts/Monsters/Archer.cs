using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    // Variable for movement
    public int walkSpeed;

    // Variables for patrol
    private bool mustPatrol;
    private bool mustTurn;

    // Variables for detecting collision
    public Transform groundCheckPos;
    public Transform objectCheckPos;
    public Transform attackPosA;
    public Transform attackPosB;
    public LayerMask groundLayer;
    public LayerMask obstacles;
    public LayerMask skeletonLayer;
    public LayerMask archerLayer;
    public Collider2D bodyCollider;
    public Rigidbody2D archer;

    // Array of colliders of the monsters
    private Collider2D[] skeletons;
    private Collider2D[] archers;

    // Variables for detecting player
    private Transform player;
    public LayerMask playerLayer;
    private Collider2D playerCollider;
    int playerHealth;
    float distToPlayer;
    public int range;

    // Variables for monster stats
    public int maxHealth = 100;
    public int currentHealth;
    bool hurt;

    // Variable for arrow
    public GameObject arrow;
    public int shootSpeed;
    private bool canShoot;

    // Animator for monster
    public Animator archerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing the monster
        mustPatrol = true;
        archer = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        playerCollider = GameObject.Find("Player").GetComponent<PlayerController>().bodyCollider;
        currentHealth = maxHealth;
        canShoot = true;
        hurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Setting walking/running animation of monster based on the condition mustPatrol
        archerAnimator.SetBool("IsRunning", mustPatrol);

        // Getting player's health to see whether player is dead or alive
        playerHealth = playerCollider.GetComponent<PlayerController>().currentHealth;

        // Checking if monster should walk/run, if should then calls the Patrol() method
        if (mustPatrol)
        {
            Patrol();
        }

        // Getting distance between monster and player
        distToPlayer = Vector2.Distance(transform.position, player.position);

        // Checking if player is within the aggro range of the monster,
        // If it is then it will chase after the player
        // If the monster or player is dead then this code will not activate
        if (distToPlayer <= range && currentHealth > 0 && playerHealth > 0)
        {
            // Checking whether player is on the left or right side of the monster,
            // Then the monster will turn to the side where the player is at
            if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            // Checking whether player is within the monster's attack range,
            // If it is then the monster will attack the player
            if (Physics2D.OverlapArea(attackPosA.position, attackPosB.position) && canShoot && !hurt)
            {
                // Forces monster to stop moving when attacking
                mustPatrol = false;
                archer.velocity = Vector2.zero;

                // Dealing damage
                StartCoroutine(Shoot());
            }
        }
        else if (currentHealth <= 0 || playerHealth <= 0)
        {
            mustPatrol = false;
            archer.velocity = Vector2.zero;
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
            // Checking if monster is still on the platform
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        // Getting the list of monsters that it has came into contact with
        skeletons = Physics2D.OverlapCircleAll(objectCheckPos.position, 0.1f, skeletonLayer);
        archers = Physics2D.OverlapCircleAll(objectCheckPos.position, 0.1f, archerLayer);

        // Checking if monster needs to turn
        if (mustTurn || bodyCollider.IsTouchingLayers(obstacles))
        {
            Flip();
        }

        // Allowing monsters to walk past each other
        if (skeletons.Length != 0)
        {
            foreach (Collider2D enemy in skeletons)
            {
                if (bodyCollider.IsTouching(enemy))
                {
                    Physics2D.IgnoreCollision(bodyCollider, enemy);
                }
            }
        }
        if (archers.Length != 0)
        {
            foreach (Collider2D enemy in archers)
            {
                if (bodyCollider.IsTouching(enemy))
                {
                    Physics2D.IgnoreCollision(bodyCollider, enemy);
                }
            }
        }

        // Setting the movement of the monster
        archer.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, archer.velocity.y);
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

        // Hurt animation
        archerAnimator.SetTrigger("Hurt");

        // Check if monster is dead,
        // If it is dead, then call the Die() method
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Hurt());
        }
    }

    void Die()
    {
        CharacterAttribute character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");

        // Death animation
        archerAnimator.SetBool("IsDead", true);

        // Disables the monster collider
        GetComponent<Collider2D>().enabled = false;

        // Heals the player
        player.GetComponent<PlayerController>().currentHealth += 10;
        if (player.GetComponent<PlayerController>().currentHealth > character.health)
        {
            player.GetComponent<PlayerController>().currentHealth = character.health;
        }

        // Gives player exp and gold
        player.GetComponent<PlayerController>().exp += 100;
        player.GetComponent<PlayerController>().gold += 200;
        character.experience += 100;
        character.gold += 200;
        DataHandler.SaveToJSON(character, "CharacterAttribute");

        // Monster revives after a set amount of time
        StartCoroutine(MonsterRespawn());
    }

    IEnumerator MonsterRespawn()
    {
        yield return new WaitForSeconds(20);

        // Disables the death animation
        archerAnimator.SetBool("IsDead", false);

        // Enables the monster collider
        GetComponent<Collider2D>().enabled = true;

        // Set the condition of monster to before death
        currentHealth = maxHealth;
        mustPatrol = true;
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        // Activates the attack animation of the monster
        archerAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.8f);

        GameObject newArrow = Instantiate(arrow, attackPosA.position, Quaternion.identity);

        if (transform.localScale.x > 0)
        {
            newArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed, 0f);
        }
        else
        {
            newArrow.transform.localScale = new Vector2(newArrow.transform.localScale.x * -1, newArrow.transform.localScale.y);
            newArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * -1, 0f);
        }

        yield return new WaitForSeconds(0.8f);

        canShoot = true;
    }

    IEnumerator Hurt()
    {
        hurt = true;

        yield return new WaitForSeconds(2);

        hurt = false;
    }
}
