using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;

    //public float bulletDamage = 10f;
    //[SerializeField] private Rigidbody2D rb;
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        if (hit)
        {
            Deactivate();
        }
        float movementSpeed = bulletSpeed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 10) gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        //Destroy(gameObject);
    }*/
}
