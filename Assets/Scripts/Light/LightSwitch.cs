using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
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
    private List<GameObject> lightSwitches = new List<GameObject>();
    private List<Color> colorsToBeAdded = new List<Color>();
    private List<GameObject> lightSwitchesToBeAdded = new List<GameObject>();

    void Start()
    {
        cc = FindObjectOfType<ColorControl>();
        parentScript = transform.parent.GetComponent<LightParent>();
        lightShades = transform.parent.transform.Find("LightShades").gameObject;
        lightRing = transform.parent.transform.Find("LightRing").gameObject;

        // Initialize original colors
        originalLightShadesColor = lightShades.GetComponent<SpriteRenderer>().color;
        originalLightRingColor = lightRing.GetComponent<SpriteRenderer>().color;

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "LightSwitch")
            {
                lightSwitches.Add(obj);
            }
        }
    }

    void Update()
    {
        float ringRadius = 4.3f * 2;
        foreach (GameObject lightSwitch in lightSwitches)
        {
            Vector3 otherLightCenter = lightSwitch.transform.position;
            Vector3 currentLightCenter = transform.position;
            float distance = Vector3.Distance(otherLightCenter, currentLightCenter);

            if (distance <= ringRadius) 
            {
                if (!colorsToBeAdded.Contains(lightSwitch.GetComponent<SpriteRenderer>().color))
                {
                    if (!cc.CompareColors(lightSwitch.GetComponent<SpriteRenderer>().color, GetComponent<SpriteRenderer>().color))
                    {
                        if (lightSwitch.transform.parent.GetComponent<LightParent>().lighted)
                        {
                            colorsToBeAdded.Add(lightSwitch.GetComponent<SpriteRenderer>().color);
                            lightSwitchesToBeAdded.Add(lightSwitch);
                        }
                    }
                }
            }

        }

        for (int i = 0; i < lightSwitchesToBeAdded.Count; i++)
        {
            GameObject lightSwitch = lightSwitchesToBeAdded[i];
            Vector3 otherLightCenter = lightSwitch.transform.position;
            Vector3 currentLightCenter = transform.position;
            float distance = Vector3.Distance(otherLightCenter, currentLightCenter);

            if ((distance > ringRadius) || !lightSwitch.transform.parent.GetComponent<LightParent>().lighted)
            {
                colorsToBeAdded.Remove(lightSwitch.GetComponent<SpriteRenderer>().color);
                lightSwitchesToBeAdded.Remove(lightSwitch);
            }
        }

        lightShades.GetComponent<SpriteRenderer>().color = UpdateColor();
        lightRing.GetComponent<SpriteRenderer>().color = UpdateColor();
    }

    private Color UpdateColor()
    {
        Color currentColor = GetComponent<SpriteRenderer>().color;
        foreach (Color color in colorsToBeAdded)
        {
            currentColor = cc.AddColor(color, currentColor);
        }
        return currentColor;
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
        /*
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
        }*/
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
        /*
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
        }*/
    }
}


