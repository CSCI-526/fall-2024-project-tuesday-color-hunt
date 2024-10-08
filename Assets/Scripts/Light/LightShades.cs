using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LightShades : MonoBehaviour
{
    private CameraMovement cm;
    private string collidedObjectName;

    void Start()
    {
        cm = FindObjectOfType<CameraMovement>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedObjectName = collision.gameObject.name;
        GameObject foundObject = GameObject.Find(collidedObjectName);

        if (foundObject != null)
        {
            Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

            if ((collision.CompareTag("Enemy") || collision.CompareTag("Obstacle")) && cm.CompareColors(objectColor, GetComponent<SpriteRenderer>().color))
            {
                foundObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
