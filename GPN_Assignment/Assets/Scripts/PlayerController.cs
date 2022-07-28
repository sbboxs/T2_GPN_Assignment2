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
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.J) && isTouchingGround)
        {
            playerAnimator.SetTrigger("Attack");
        }
    }
}
