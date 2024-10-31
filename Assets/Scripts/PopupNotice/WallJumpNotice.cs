using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpNotice : MonoBehaviour
{
    public GameObject uiImage;
    public GameObject text;

    private int index;

    void Start()
    {
        if (uiImage != null) uiImage.SetActive(false);
        if (text != null) text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiImage.SetActive(true);
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiImage.SetActive(false);
            text.SetActive(false);
        }
    }
}
