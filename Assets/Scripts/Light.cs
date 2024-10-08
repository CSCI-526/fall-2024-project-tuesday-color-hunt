using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private Transform grabPosition;
    private GameObject lightShades;
    public bool playerTouched = false;
    private bool lighted = false;
    private bool isGrabbing = false;

    void Start()
    {
        lightShades = transform.Find("LightShades").gameObject;
    }

    void Update()
    {
        if (playerTouched)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (lighted)
                {
                    lighted = false;
                    lightShades.SetActive(false);
                }
                else
                {
                    lighted = true;
                    lightShades.SetActive(true);
                }
            }

            if (Input.GetKey(KeyCode.K) && !isGrabbing) // Only allow grabbing if not already grabbing
            {
                transform.parent= grabPosition;
                transform.position = grabPosition.position;
                GetComponent<Rigidbody2D>().isKinematic = true; // No gravity
                isGrabbing = true;
            }
            else if (!Input.GetKey(KeyCode.K) && isGrabbing) // Release the object when 'K' is released
            {
                transform.parent = null;
                GetComponent<Rigidbody2D>().isKinematic = false;
                isGrabbing = false;
            }

        }
    }

   
}
