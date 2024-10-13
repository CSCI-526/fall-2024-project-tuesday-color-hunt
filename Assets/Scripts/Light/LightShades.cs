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
    
    void Start()
    {
        cc = FindObjectOfType<ColorControl>();
    }

    void Update()
    {
        for(int i = 0; i < objectsCollided.Count; i++)
        {
            GameObject obj = objectsCollided[i];
            Color objectColor = obj.GetComponent<SpriteRenderer>().color;
            if ((obj.CompareTag("Enemy") || obj.CompareTag("Obstacle")) && cc.CompareColors(objectColor, GetComponent<SpriteRenderer>().color))
            {
                //objectsCollided.Remove(obj);
                obj.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsCollided.Add(collision.gameObject);    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsCollided.Remove(collision.gameObject);
    }
}
