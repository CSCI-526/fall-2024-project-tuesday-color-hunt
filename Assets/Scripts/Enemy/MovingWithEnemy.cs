using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MovingWithEnemy : MonoBehaviour
{
    [SerializeField] private Transform initialGrabParent;
    [SerializeField] private float x_pos;
    [SerializeField] private float y_pos;

    private bool isCollided;
    private Rigidbody2D rb; // For 2D Rigidbody
    private Vector3 previousPosition; // Track previous position to calculate velocity
    private Rigidbody2D otherRb;
    private Coroutine resetVelocityCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
    }

    void Update()
    {
        if (initialGrabParent && initialGrabParent.gameObject.activeInHierarchy)
        {
            Vector3 offset = new Vector3(x_pos, y_pos, 0.0f);
            transform.position = initialGrabParent.position + offset;
        }
    }

    private void FixedUpdate()
    {
        // Calculate the velocity based on position change
        Vector3 currentVelocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
        previousPosition = transform.position;

        // Update the Rigidbody velocity
        if (rb != null)
        {
            rb.velocity = currentVelocity;
        }

        if (isCollided)
        {
            if (otherRb != null)
            {
                print("ok");
                otherRb.velocity += rb.velocity;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            isCollided = true;

            // Stop any existing coroutine in case of rapid collisions
            if (resetVelocityCoroutine != null)
            {
                StopCoroutine(resetVelocityCoroutine);
                resetVelocityCoroutine = null;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (otherRb != null)
            {
                // Start a coroutine to reset the velocity after 2 seconds
                resetVelocityCoroutine = StartCoroutine(ResetVelocityAfterDelay(2f));
            }
            isCollided = false;
        }
    }

    private IEnumerator ResetVelocityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (otherRb != null)
        {
            otherRb.velocity = Vector2.zero; // Reset player velocity after delay
        }
    }
}
