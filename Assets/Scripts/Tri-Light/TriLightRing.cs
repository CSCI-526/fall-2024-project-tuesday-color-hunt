using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class TriLightRing : MonoBehaviour
{
    private float forceAmount = 500f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Rigidbody2D obstacleRigidbody = collision.GetComponent<Rigidbody2D>();

            if (obstacleRigidbody != null)
            {
                obstacleRigidbody.constraints = RigidbodyConstraints2D.None;

                Vector2 forceDirection = (transform.position - collision.transform.position).normalized;

                obstacleRigidbody.AddForce(forceDirection * forceAmount);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Rigidbody2D obstacleRigidbody = collision.GetComponent<Rigidbody2D>();

            if (obstacleRigidbody != null)
            {
                obstacleRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
