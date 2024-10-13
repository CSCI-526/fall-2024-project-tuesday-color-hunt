using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParent : MonoBehaviour
{
    private GameObject lightSwitch;
    private GameObject lightShades;
    private GameObject lightRing;

    public bool lighted = false;
    public bool playerTouched = false;
    [SerializeField] private Transform grabPosition;

    private PlayerMovement pm;

    void Start()
    {
        lightShades = transform.Find("LightShades").gameObject;
        lightSwitch = transform.Find("LightSwitch").gameObject;
        lightRing = transform.Find("LightRing").gameObject;
        lightShades.SetActive(false);
        lightRing.SetActive(false);
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (playerTouched)  
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                lighted = !lighted;
                lightShades.SetActive(lighted);
                lightRing.SetActive(lighted);
            }

            if (Input.GetKey(KeyCode.K) && !pm.isGrabbing)
            {
                transform.parent = grabPosition;
                transform.position = grabPosition.position;
                lightSwitch.GetComponent<Rigidbody2D>().isKinematic = true; // No gravity
                pm.isGrabbing = true;
            }
            else if (!Input.GetKey(KeyCode.K) && pm.isGrabbing)
            {
                transform.parent = null;
                transform.position = transform.position;
                lightSwitch.GetComponent<Rigidbody2D>().isKinematic = false; // No gravity
                pm.isGrabbing = false;
            }
        }

    }
}
