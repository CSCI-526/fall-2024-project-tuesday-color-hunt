using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColorNotice : MonoBehaviour
{
    public GameObject uiImage;
    public GameObject text;

    private bool isPopUpActive = false;
    private bool hasShownOnce = false; 

    void Start()
    {
        if (uiImage != null) uiImage.SetActive(false);
        if (text != null) text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasShownOnce)
        {
            uiImage.SetActive(true);
            text.SetActive(true);
            isPopUpActive = true;      
            hasShownOnce = true;        
        }
    }

    private void Update()
    {
        if (isPopUpActive && Input.anyKeyDown)
        {
            uiImage.SetActive(false);
            text.SetActive(false);
            isPopUpActive = false;    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiImage.SetActive(false);
            text.SetActive(false);
            isPopUpActive = false;      
        }
    }
}
