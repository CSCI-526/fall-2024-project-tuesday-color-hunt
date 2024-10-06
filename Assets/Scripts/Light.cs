using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    // Reference to the Main Camera
    private Camera mainCamera;
    private CameraMovement cm;

    public bool lighted = false;
    [SerializeField] private int r;
    [SerializeField] private int g;
    [SerializeField] private int b;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the main camera at the start
        mainCamera = Camera.main;
        cm = FindObjectOfType<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lighted)
        {
            lighted = false;
            cm.MinusColor(r, g, b);
        }
        else
        {
            lighted = true;
            cm.AddColor(r, g, b);
        }

        // }
    }
}
