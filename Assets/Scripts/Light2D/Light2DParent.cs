using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DParent : MonoBehaviour
{
    private GameObject LightSwitch;
    private Light2D Light2D;

    public bool lighted = false;
    public bool playerTouched = false;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private Transform initialGrabParent; // The object that might initially grab this

    private PlayerMovement pm;
    private bool isGrabbedByEnemy;

    void Start()
    {
        Light2D = transform.Find("Light2D").gameObject.GetComponent<Light2D>();
        LightSwitch = transform.Find("LightSwitch").gameObject;
        if (!lighted)
        {
            Light2D.intensity = 0;
        }
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        // Check if the initialGrabParent has become inactive
        if (initialGrabParent && initialGrabParent.gameObject.activeInHierarchy)
        {
            transform.position = initialGrabParent.position;
        }

        if (playerTouched && !isGrabbedByEnemy)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                lighted = !lighted;
                if (lighted)
                {
                    Light2D.intensity = 1;
                }
                else
                {
                    Light2D.intensity = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (pm.isGrabbing)
                {
                    ReleaseObject();
                }
                else
                {
                    GrabObject();
                }
            }
        }
    }

    private void GrabObject()
    {
        transform.parent = grabPosition;
        transform.position = grabPosition.position;
        LightSwitch.GetComponent<Rigidbody2D>().isKinematic = true; // Disable gravity
        pm.isGrabbing = true;
    }

    private void ReleaseObject()
    {
        transform.SetParent(null); // Detach the object from its parent without moving it
        LightSwitch.GetComponent<Rigidbody2D>().isKinematic = false; // Enable gravity
        pm.isGrabbing = false;
    }

}
