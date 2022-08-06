using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTownPlayerController : MonoBehaviour
{
    public float walkSpeed, jumpVelocity;
    private Rigidbody2D p;
    private bool isTouchingGround;

    public Collider2D bodyCollider;
    public LayerMask ground;
    public Animator playerAnimator;
    public LayerMask enemyLayers;
    public Rigidbody2D enemy;

    public Transform collideEnemy;

    //Variable for attack
    public Transform AttackPoint;
    public float attackRange = 0.5f;

    public float attackRate = 1.1f;
    public float attackRateBoost = 0f; //Get from item
    float nextAttackTime = 0f;

    //Variable for character status
    public int maxHealth = 100;
    public int equipmentHealth;
    int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // Allow player to walk through monster
            Physics2D.IgnoreLayerCollision(7, 9);

            isTouchingGround = bodyCollider.IsTouchingLayers(ground);
            float direction = Input.GetAxisRaw("Horizontal");
            float jump = Input.GetAxisRaw("Vertical");

            p.velocity = new Vector2(walkSpeed * direction * Time.fixedDeltaTime, p.velocity.y);
            // Running
            if (direction != 0f)
            {
                playerAnimator.SetBool("IsRunning", true);
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);
            }
            else
            {
                playerAnimator.SetBool("IsRunning", false);
            }

            // Jumping
            if (jump > 0 && isTouchingGround)
            {
                playerAnimator.SetBool("Jump", true);
                p.velocity = new Vector2(p.velocity.x, jumpVelocity * jump * Time.fixedDeltaTime);
            }
            else if (jump == 0 && isTouchingGround)
            {
                playerAnimator.SetBool("Jump", false);
            }

            //Limiting Attack rate
            if (Time.time >= nextAttackTime)
            {
                // Attacking
                if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
                {
                    nextAttackTime = Time.time + (attackRate - attackRateBoost);  //TIme of now + attack rate

                    // Player animator
                    playerAnimator.SetTrigger("Attack");

                    // Detect enemies in range of attack
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

                    if (hitEnemies.Length != 0)
                    {
                        //Damage enemies
                        foreach (Collider2D enemy in hitEnemies)
                        {
                            enemy.GetComponent<Skeleton>().TakeDamage(20);
                        }
                    }
                }
            }

        }
        else
        {
            return;
        }

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Hurt Animation
        playerAnimator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        //Die animation
        playerAnimator.SetBool("IsDead", true);
        //Disable the player.
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
