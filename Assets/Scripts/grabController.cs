using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class grabController : MonoBehaviour
{
    [SerializeField] private Transform grabDetect;
    [SerializeField] private Transform grabPosition;
    private float rayDist;

    public bool lighted = false;
    private GameObject light;

    void Start()
    {

    }

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        if (grabCheck.collider != null && grabCheck.collider.tag == "Light")
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                light = grabCheck.collider.gameObject.transform.Find("LightShades").gameObject;
                if (lighted)
                {
                    lighted = false;
                    light.SetActive(false);

                    //cm.MinusColor(r, g, b);
                }
                else
                {
                    lighted = true;
                    light.SetActive(true);
                    //cm.AddColor(r, g, b);
                }
            }
            if (Input.GetKey(KeyCode.K))
            {
                grabCheck.collider.gameObject.transform.parent = grabPosition;
                grabCheck.collider.gameObject.transform.position = grabPosition.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true; // no gravity
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
