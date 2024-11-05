using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class LightShades : MonoBehaviour
{
    private ColorControl cc;
    private List<GameObject> objectsCollided = new List<GameObject>();
    private List<GameObject> objectsInDome = new List<GameObject>();

    void Start()
    {
        cc = FindObjectOfType<ColorControl>();
    }

    void Update()
    {
        for(int i = 0; i < objectsCollided.Count; i++)
        {
            GameObject obj = objectsCollided[i];
            if (obj.GetComponent<SpriteRenderer>())
            {
                Color objectColor = obj.GetComponent<SpriteRenderer>().color;
                if (obj.CompareTag("Enemy") || obj.CompareTag("Obstacle"))
                {
                    if (cc.CompareColors(objectColor, GetComponent<SpriteRenderer>().color))
                    {
                        obj.SetActive(false);
                    }
                }
                else if (obj.CompareTag("Enemy") && !cc.CompareColors(objectColor, GetComponent<SpriteRenderer>().color)) 
                {
                    if (gameObject.CompareTag("LightShades"))
                    {
                        obj.GetComponent<EnemyPatrol>().enemyInsideDome = true;
                        objectsInDome.Add(obj);
                    }
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsCollided.Add(collision.gameObject);    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectsCollided.Contains(collision.gameObject))
        {
            objectsCollided.Remove(collision.gameObject);
        }
        
    }
}
