using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private ColorControl cc;
    private LightParent parentScript;
    private GameObject lightShades;
    private GameObject lightRing;

    // Store the original colors
    private Color originalLightShadesColor;
    private Color originalLightRingColor;

    void Start()
    {
        cc = FindObjectOfType<ColorControl>();
        parentScript = transform.parent.GetComponent<LightParent>();
        lightShades = transform.parent.transform.Find("LightShades").gameObject;
        lightRing = transform.parent.transform.Find("LightRing").gameObject;

        // Initialize original colors
        originalLightShadesColor = lightShades.GetComponent<SpriteRenderer>().color;
        originalLightRingColor = lightRing.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        GameObject foundObject = GameObject.Find(collidedObjectName);
        Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

        if (collision.CompareTag("Player"))
        {
            if (parentScript != null)
            {
                parentScript.playerTouched = true;
            }
        }
        if (collision.CompareTag("LightShades") || collision.CompareTag("LightRing"))
        {
            Color objectDefaultColor = foundObject.transform.parent.Find("LightSwitch").gameObject.GetComponent<SpriteRenderer>().color;

            if (!cc.CompareColors(objectDefaultColor, GetComponent<SpriteRenderer>().color))
            {
                GameObject otherLightParent = collision.transform.parent.gameObject;

                // Check if the collided object is from a different parent (different light system)
                if (otherLightParent != transform.parent.gameObject)
                {
                    // Change the colors
                    lightShades.GetComponent<SpriteRenderer>().color = cc.AddColor(objectDefaultColor, GetComponent<SpriteRenderer>().color);
                    lightRing.GetComponent<SpriteRenderer>().color = cc.AddColor(objectDefaultColor, GetComponent<SpriteRenderer>().color);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        GameObject foundObject = GameObject.Find(collidedObjectName);
        Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

        if (collision.CompareTag("Player"))
        {
            if (parentScript != null)
            {
                parentScript.playerTouched = false;
            }
        }
        if (collision.CompareTag("LightShades") || collision.CompareTag("LightRing"))
        {
            Color objectDefaultColor = foundObject.transform.parent.Find("LightSwitch").gameObject.GetComponent<SpriteRenderer>().color;

            if (!cc.CompareColors(objectDefaultColor, GetComponent<SpriteRenderer>().color))
            {
                GameObject otherLightParent = collision.transform.parent.gameObject;

                // Check if the collided object is from a different parent (different light system)
                if (otherLightParent != transform.parent.gameObject)
                {
                    // Restore the original colors
                    lightShades.GetComponent<SpriteRenderer>().color = originalLightShadesColor;
                    lightRing.GetComponent<SpriteRenderer>().color = originalLightRingColor;
                }
            }
        }
    }
}


