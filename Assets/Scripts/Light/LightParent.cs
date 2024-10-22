using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParent : MonoBehaviour
{
    private GameObject LightSwitch;
    private GameObject LightShades;
    private GameObject LightRing;

    public bool lighted = false;
    public bool playerTouched = false;
    [SerializeField] private Transform grabPosition;

    private PlayerMovement pm;
    private bool isGrabbing = false;

    void Start()
    {
        LightShades = transform.Find("LightShades").gameObject;
        LightSwitch = transform.Find("LightSwitch").gameObject;
        LightRing = transform.Find("LightRing").gameObject;
        LightShades.SetActive(false);
        LightRing.SetActive(false);
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (playerTouched)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                lighted = !lighted;
                LightShades.SetActive(lighted);
                LightRing.SetActive(lighted);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (isGrabbing)
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
        isGrabbing = true;
    }

    private void ReleaseObject()
    {
        transform.parent = null;
        LightSwitch.GetComponent<Rigidbody2D>().isKinematic = false; // Enable gravity
        pm.isGrabbing = false;
        isGrabbing = false;
    }
}
