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

            if (Input.GetKey(KeyCode.K) && !pm.isGrabbing)
            {
                transform.parent = grabPosition;
                transform.position = grabPosition.position;
                triLightSwitch.GetComponent<Rigidbody2D>().isKinematic = true; // No gravity
                pm.isGrabbing = true;
            }
            else if (!Input.GetKey(KeyCode.K) && pm.isGrabbing)
            {
                transform.parent = null;
                transform.position = transform.position;
                triLightSwitch.GetComponent<Rigidbody2D>().isKinematic = false; // No gravity
                pm.isGrabbing = false;
            }
        }

    }
}

