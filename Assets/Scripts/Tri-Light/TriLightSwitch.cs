using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriLightSwitch : MonoBehaviour
{
    private TriLightParent parentScript;
    private GameObject triLightShades;
    private GameObject triLightRing;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = transform.parent.GetComponent<TriLightParent>();
        triLightShades = transform.parent.transform.Find("TriLightShades").gameObject;
        triLightRing = transform.parent.transform.Find("TriLightRing").gameObject;
    }

    // Update is called once per frame
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
