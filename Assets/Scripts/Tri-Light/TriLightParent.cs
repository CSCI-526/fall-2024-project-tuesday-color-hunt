using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriLightParent : MonoBehaviour
{
    private GameObject triLightSwitch;
    private GameObject triLightShades;
    private GameObject triLightRing;

    public bool lighted = false;
    public bool playerTouched = false;
    [SerializeField] private Transform grabPosition;

    private PlayerMovement pm;
    private bool isGrabbing = false;

    void Start()
    {
        triLightShades = transform.Find("TriLightShades").gameObject;
        triLightSwitch = transform.Find("TriLightSwitch").gameObject;
        triLightRing = transform.Find("TriLightRing").gameObject;
        triLightShades.SetActive(false);
        triLightRing.SetActive(false);
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (playerTouched)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                lighted = !lighted;
                triLightShades.SetActive(lighted);
                triLightRing.SetActive(lighted);
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
        triLightSwitch.GetComponent<Rigidbody2D>().isKinematic = true; // Disable gravity
        pm.isGrabbing = true;
        isGrabbing = true;
    }

    private void ReleaseObject()
    {
        transform.parent = null;
        triLightSwitch.GetComponent<Rigidbody2D>().isKinematic = false; // Enable gravity
        pm.isGrabbing = false;
        isGrabbing = false;
    }
}
