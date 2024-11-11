using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MovingWithEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetAboveTarget;
    [SerializeField] private float offsetAsideTarget;
    private PlayerMovement pm;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (target && target.gameObject.activeInHierarchy && !CompareTag("Light"))
        {
            transform.position = new Vector3(target.position.x + offsetAsideTarget, target.position.y + offsetAboveTarget, target.position.z);
        }
        if (pm.isGrabbing && CompareTag("Light"))
        {

        }
        else if (!pm.isGrabbing && CompareTag("Light"))
        {
            transform.position = new Vector3(target.position.x + offsetAsideTarget, target.position.y + offsetAboveTarget, target.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !CompareTag("Light") && offsetAsideTarget == 0) 
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !CompareTag("Light") && offsetAsideTarget == 0)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                collision.transform.SetParent(null);
            }
            else
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !CompareTag("Light") && offsetAsideTarget == 0)
        {
            collision.transform.SetParent(null);
        }
    }
}
