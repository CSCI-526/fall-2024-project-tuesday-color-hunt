using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DSwitch : MonoBehaviour
{
    private ColorControl cc;
    private Light2DParent parentScript;
    private Light2D light2D;

    private int lightMixedCount;
    private PlayerMovement pm;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cc = FindObjectOfType<ColorControl>();
        parentScript = transform.parent != null ? transform.parent.GetComponent<Light2DParent>() : null;

        Transform lightTransform = transform.Find("Light2D");
        if (lightTransform != null)
        {
            light2D = lightTransform.GetComponent<Light2D>();
        }


        lightMixedCount = PlayerPrefs.GetInt("lightMixedCount", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (parentScript != null)
            {
                parentScript.playerTouched = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (parentScript != null)
            {
                parentScript.playerTouched = false;
            }
        }
    }
}
