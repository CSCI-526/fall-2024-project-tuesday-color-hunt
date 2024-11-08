using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyPatrolVertical : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 3f; 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask lightRingLayer;
    [SerializeField] private LayerMask lightShadesLayer;

    private bool movingRight = true;
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform player; 

    private Rigidbody2D rb;

    //private bool isChasingPlayer = false;
    public bool enemyInsideDome = false;
    //public bool enemyhitLightRing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();

        /*
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && !enemyInsideDome)
        {
            isChasingPlayer = true;
        }
        else
        {
            isChasingPlayer = false;
        }

        if (!isChasingPlayer)
        {
            Patrol();
        }
        else
        {
            ChasePlayer();
        }*/
    }

    private void Patrol()
    {
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed * (movingRight ? 1 : -1));

        if (atEdge() || hittingWall() || (hittingRing() && !hittingShades()))
        {
            Flip();
        }
    }

    /*
    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (atEdge() || hittingWall() || (hittingRing() && !hittingShades()))
        {
            Flip();
        }

        if (direction.x > 0 && !movingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && movingRight)
        {
            Flip();
        }
    }*/

        private bool atEdge()
    {
        return !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private bool hittingWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);
    }

    
    private bool hittingRing()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, lightRingLayer);
    }

    private bool hittingShades()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, lightShadesLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Flip();
        }
    }


    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
