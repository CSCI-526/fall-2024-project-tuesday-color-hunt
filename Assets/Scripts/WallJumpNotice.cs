using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class WallJumpNotice : MonoBehaviour
{
    private GameObject dialogueBox;
    private GameObject text;

    private int index;

    void Start()
    {
        dialogueBox = transform.Find("DialogueCanvas").gameObject.transform.Find("DialogueBox").gameObject;
        text = transform.Find("DialogueCanvas").gameObject.transform.Find("Text").gameObject;
        if (dialogueBox) dialogueBox.SetActive(false);
        if (text) text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            text.SetActive(false);
        }
    }
}
