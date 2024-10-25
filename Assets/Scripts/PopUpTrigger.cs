using UnityEngine;
using System.Collections;

public class PopUpTrigger : MonoBehaviour
{
    public GameObject popUpPanel;  // Reference to the Pop-Up UI Panel
    public float popUpDuration = 3f;  // Duration before the pop-up disappears
    private bool isPopUpActive = false;  // Flag to check if the pop-up is active

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Show the pop-up notice
            popUpPanel.SetActive(true);
            isPopUpActive = true;  // Set flag to true
            StartCoroutine(HidePopUpAfterDelay());
        }
    }

    public void Update()
    {
        // If pop-up is active, close it when any key is pressed
        if (isPopUpActive && Input.anyKeyDown)
        {
            popUpPanel.SetActive(false);  // Hide pop-up
            isPopUpActive = false;  // Reset the flag
            StopCoroutine(HidePopUpAfterDelay());  // Stop auto-hide if player presses a key
        }
    }

    // Coroutine to hide the pop-up after a delay
    public IEnumerator HidePopUpAfterDelay()
    {
        yield return new WaitForSeconds(popUpDuration);
        popUpPanel.SetActive(false);
        isPopUpActive = false;  // Reset the flag
    }
}
