using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    private Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn)
        {
            Flip();
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
}
