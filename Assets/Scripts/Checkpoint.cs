using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerMovement pm;
    [SerializeField] private Sprite newCheckpoint;
    private Sprite originalCheckpoint;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        originalCheckpoint = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        Vector3 respawn = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        if (pm.reachCheck && pm.respawnPosition != respawn)
        {
            GetComponent<SpriteRenderer>().sprite = originalCheckpoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pm.reachCheck = true;
            Vector3 checkpointPosition = transform.position;
            pm.respawnPosition = new Vector3(checkpointPosition.x, checkpointPosition.y + 1.5f, checkpointPosition.z);
            SpriteRenderer checkpointSprite = GetComponent<SpriteRenderer>();
            if (checkpointSprite != null)
            {
                checkpointSprite.sprite = newCheckpoint;
            }
        }
    }
}
