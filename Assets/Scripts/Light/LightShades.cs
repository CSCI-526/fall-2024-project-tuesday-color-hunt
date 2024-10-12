using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class LightShades : MonoBehaviour
{
    private ColorControl cc;

    void Start()
    {
        cc = FindObjectOfType<ColorControl>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        GameObject foundObject = GameObject.Find(collidedObjectName);

        if (foundObject != null)
        {
            Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

            if ((collision.CompareTag("Enemy") || collision.CompareTag("Obstacle")) && cc.CompareColors(objectColor, GetComponent<SpriteRenderer>().color))
            {
                foundObject.SetActive(false);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        GameObject foundObject = GameObject.Find(collidedObjectName);

        if (foundObject != null)
        {
            Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

        }
    }
}
