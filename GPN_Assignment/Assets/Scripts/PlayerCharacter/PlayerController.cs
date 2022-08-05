using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed, jumpVelocity;
    private Rigidbody2D p;
    private bool isTouchingGround;

    public Collider2D bodyCollider;
    public LayerMask ground;
    public Animator playerAnimator;
    public LayerMask enemyLayers;

    //Variable for attack
    public Transform AttackPoint;
    public float attackRange = 0.5f;
   
    public float attackRate = 0.2f;
    public float attackRateBoost = 0f; //Get from item

    //Variable for character status
    public int maxHealth = 100;
    private int equipmentHealth;
    public int currentHealth;
    int atkDMG = 20;
    bool attacking = false;
    int lvl = 1;
    public int exp = 0;
    int lvlUp;
    int attackCount = 1;


    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth + equipmentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            // Lvling up
            lvlUp = lvl * 50;

            if (exp >= lvlUp)
            {
                exp = exp - lvlUp;
                lvl += 1;
                atkDMG += 10;
                maxHealth += 20;
                currentHealth = maxHealth;
                Debug.Log(lvl);
                Debug.Log(atkDMG);
                Debug.Log(currentHealth);
            }
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
            
            // Attacking
            if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround && !attacking && attackCount == 1)
            {
                StartCoroutine(Attacking());
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround && !attacking && attackCount == 2)
            {
                StartCoroutine(Attacking());
            }
            //else if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround && attacking)
            //{

            //}

            // Opening inventory
            //if (Input.GetKeyDown(KeyCode.E))
            //{

            //}
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
        //Die animation
        playerAnimator.SetBool("IsDead", true);
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    IEnumerator Attacking()
    {
        attacking = true;

        if (attackCount == 1)
        {
            // Player animator
            playerAnimator.SetTrigger("Attack");
            attackCount = 2;
        }
        else
        {
            // Player animator
            playerAnimator.SetTrigger("Attack2");
            attackCount = 1;
        }

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        if (hitEnemies.Length != 0)
        {
            //Damage enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<AIPatrol>().TakeDamage(atkDMG);
            }
        }

        yield return new WaitForSeconds(0.6f);

        attacking = false;
    }
}
