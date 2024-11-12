using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask voidLayer;
    [SerializeField] private LayerMask lightRingLayer;
    [SerializeField] private LayerMask lightShadesLayer;

    private bool movingRight = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform player;

    private Rigidbody2D rb;

    private bool enemyWasInsideDome = false;
    private bool enemyInsideDome;
    [SerializeField] private bool patrolVoid;
    private ColorControl cc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = FindObjectOfType<ColorControl>();
    }

    void Update()
    {
        Patrol();

        bool currentlyInsideDome = hittingShades();

        if (currentlyInsideDome != enemyWasInsideDome)
        {
            enemyInsideDome = currentlyInsideDome;

            if (enemyInsideDome)
            {
                int enemyTrappedCount = PlayerPrefs.GetInt("EnemyTrappedCount", 0);
                enemyTrappedCount++;
                PlayerPrefs.SetInt("EnemyTrappedCount", enemyTrappedCount);
                PlayerPrefs.Save();
            }

            enemyWasInsideDome = currentlyInsideDome;
        }
    }

    private void Patrol()
    {
        rb.velocity = new Vector2(moveSpeed * (movingRight ? 1 : -1), rb.velocity.y);

        if (!patrolVoid)
        {
            if (atGroundEdge() || hittingWall() || (hittingRing() && !hittingShades()))
            {
                Flip();
            }
        }
        else
        {
            if (atVoidEdge() || hittingWall() || (hittingRing() && !hittingShades()))
            {
                Flip();
            }
        }

    }

    private bool atVoidEdge()
    {
        return !Physics2D.OverlapCircle(groundCheck.position, 0.1f, voidLayer);
    }

    private bool atGroundEdge()
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
}