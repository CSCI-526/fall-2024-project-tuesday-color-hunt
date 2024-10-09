using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class LightShades : MonoBehaviour
{
    private ColorControl cc;
    private Color defaultColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        cc = FindObjectOfType<ColorControl>();
        defaultColor = GetComponent<SpriteRenderer>().color;
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
            
            if (collision.CompareTag("LightSwitch") && !cc.CompareColors(objectColor, defaultColor))
            {
                // GetComponent<SpriteRenderer>().color = cc.AddColor(objectColor, defaultColor);
                GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        GameObject foundObject = GameObject.Find(collidedObjectName);
        if (foundObject != null)
        {
            Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

            if (collision.CompareTag("LightSwitch") && !cc.CompareColors(objectColor, spriteRenderer.color))
            {
                print("light collided!");
                spriteRenderer.color = cc.AddColor(objectColor, defaultColor);
            }
        }
    }*/


    private void OnTriggerExit2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        GameObject foundObject = GameObject.Find(collidedObjectName);

        if (foundObject != null)
        {
            Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

            if (collision.CompareTag("LightSwitch") && !cc.CompareColors(objectColor, defaultColor))
            {
               // GetComponent<SpriteRenderer>().color = cc.MinusColor(objectColor, defaultColor);
            }
        }
    }
}
