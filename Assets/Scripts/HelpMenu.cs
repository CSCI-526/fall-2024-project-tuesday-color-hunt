using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public GameObject helpPopup;  // Reference to the Help Popup Panel

    public void ToggleHelp()
    {
        // Toggle the active state of the Help Popup window
        helpPopup.SetActive(!helpPopup.activeSelf);

        // Check if the Help Popup is active to show/hide the cursor
        if (helpPopup.activeSelf)
        {
            // Show the cursor
            Cursor.visible = true;
        }
        else
        {
            // Hide the cursor
            Cursor.visible = false;
        }
    }
}
