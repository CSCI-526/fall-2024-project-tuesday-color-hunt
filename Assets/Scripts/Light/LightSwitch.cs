using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private LightParent parentScript;


    void Start()
    {
        parentScript = transform.parent.GetComponent<LightParent>();
    }

    void Update()
    {
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
