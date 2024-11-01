using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LightParent : MonoBehaviour
{
    private GameObject LightSwitch;
    private GameObject LightShades;
    private GameObject LightRing;

    public bool lighted = false;
    public bool playerTouched = false;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private Transform initialGrabParent; // The object that might initially grab this

    private PlayerMovement pm;
    private bool isGrabbedByEnemy;
    private int lightToggleCount;

    void Start()
    {
        LightShades = transform.Find("LightShades").gameObject;
        LightSwitch = transform.Find("LightSwitch").gameObject;
        LightRing = transform.Find("LightRing").gameObject;
        if (!lighted)
        {
            LightShades.SetActive(false);
            LightRing.SetActive(false);
        }
        pm = FindObjectOfType<PlayerMovement>();
        lightToggleCount = PlayerPrefs.GetInt("lightToggleCount", 0);
    }

    void Update()
    {
        // Check if the initialGrabParent has become inactive
        if (initialGrabParent && initialGrabParent.gameObject.activeInHierarchy )
        {
            transform.position = initialGrabParent.position; 
        }
        
        if (playerTouched && !isGrabbedByEnemy)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                lightToggleCount++;
                PlayerPrefs.SetInt("lightToggleCount", lightToggleCount);
                PlayerPrefs.Save();

                lighted = !lighted;
                LightShades.SetActive(lighted);
                LightRing.SetActive(lighted);
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
