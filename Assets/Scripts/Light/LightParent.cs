using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParent : MonoBehaviour
{
    private GameObject lightSwitch;
    private GameObject lightShades;
    private GameObject lightRing;

    private bool lighted = false;
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
        if (Input.GetKeyDown(KeyCode.L) && playerTouched)
        {
            if (lighted)
            {
                lighted = false;
                lightShades.SetActive(false);
                lightRing.SetActive(false);
            }
            else
            {
                lighted = true;
                lightShades.SetActive(true);
                lightRing.SetActive(true);
            }
        }

        if (Input.GetKey(KeyCode.K) && !pm.isGrabbing && playerTouched) 
        {
            transform.parent = grabPosition;
            transform.position = grabPosition.position;
            lightSwitch.GetComponent<Rigidbody2D>().isKinematic = true; // No gravity
            pm.isGrabbing = true; 
        }
        else if (!Input.GetKey(KeyCode.K) && pm.isGrabbing && playerTouched) 
        {
            transform.parent = null;
            transform.position = transform.position;
            lightSwitch.GetComponent<Rigidbody2D>().isKinematic = false; // No gravity
            pm.isGrabbing = false; 
        }
    }
}
