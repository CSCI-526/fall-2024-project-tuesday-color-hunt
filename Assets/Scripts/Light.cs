using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class Light : MonoBehaviour
{
    // Reference to the Main Camera
    private Camera mainCamera;
    private CameraMovement cm;
    private PlayerMovement pm;

    public bool lighted = false;
    [SerializeField] private int r;
    [SerializeField] private int g;
    [SerializeField] private int b;

    private bool isInTrigger = false;

    private GameObject light;
    private string collidedObjectName;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the main camera at the start
        mainCamera = Camera.main;
        cm = FindObjectOfType<CameraMovement>();
        light = transform.Find("LightShades").gameObject;
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            GameObject foundObject = GameObject.Find(collidedObjectName);
            Color objectColor = foundObject.GetComponent<SpriteRenderer>().color;

            if (cm.CompareColors(objectColor, GetComponent<SpriteRenderer>().color))
            {
                foundObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle")) 
        {
            isInTrigger = true;

            print("collides");
            collidedObjectName = collision.gameObject.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            isInTrigger = false;
        }

    }
}
