using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 8f;
    private float jumpingPower = 16f;
    public bool isFacingRight = true;
    public bool canMove;

    private bool doubleJump;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private BoxCollider2D boxCollider;
    [SerializeField] private Transform startPosition;

    private Camera mainCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        canMove = true;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (canMove)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            if (isGrounded() && (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Jump")))
            {
                doubleJump = false;
            }

            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.J))
            {
                //(doubleJump && finishline.canDoubleJump)
                if (isGrounded() || doubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    doubleJump = !doubleJump;
                }

            }

            if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.J)) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            WallSlide();
            WallJump();

            if (!isWallJumping)
            {
                Flip();
            }
        }


    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
    }


    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    private void WallSlide()
    {
        if (isWalled() && !isGrounded() && horizontalInput != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
            isWallSliding = false;
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.J)) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //transform.position = startPosition.position; // Move player back to start position
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
