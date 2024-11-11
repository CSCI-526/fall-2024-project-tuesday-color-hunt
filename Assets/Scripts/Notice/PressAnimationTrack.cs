using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PressAnimationTrack : MonoBehaviour
{
    public Transform target; // The target object
    public GameObject arrow;
    public GameObject pressK;
    public GameObject pressL;
    public GameObject slash;

    private float offsetAboveTarget = 1.2f; // Distance above the target object
    private bool isStartColor = true;
    private float timer = 0f;
    private float switchInterval = 0.5f;

    private Color arrowStartColor = new Color(1f, 1f, 1f, 1f);
    private Color buttonStartColor = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
    private Color endColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);

    private PlayerMovement pm;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        arrow.SetActive(true);
    }

    void Update()
    {
        if (pm.isGrabbing)
        {
            arrow.SetActive(false);
            pressK.SetActive(false);
            pressL.SetActive(false);
            slash.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);
            pressK.SetActive(true);
            pressL.SetActive(true);
            slash.SetActive(true);
        }

        if (target != null && arrow.activeInHierarchy)
        {
            // Set the position to be directly above the target with the specified offset
            arrow.transform.position = new Vector3(target.position.x, target.position.y + offsetAboveTarget, target.position.z);
            pressK.transform.position = new Vector3(target.position.x -1f, target.position.y + offsetAboveTarget + 1.2f, target.position.z);
            pressL.transform.position = new Vector3(target.position.x +1f, target.position.y + offsetAboveTarget + 1.2f, target.position.z);
            slash.transform.position = new Vector3(target.position.x, target.position.y - 17.2f, target.position.z);

            timer += Time.deltaTime;

            // Check if the switch interval has been reached
            if (timer >= switchInterval)
            {
                // Switch colors
                arrow.GetComponent<SpriteRenderer>().color = isStartColor ? endColor : arrowStartColor;
                pressK.GetComponent<SpriteRenderer>().color = isStartColor ? endColor : buttonStartColor;
                pressL.GetComponent<SpriteRenderer>().color = isStartColor ? endColor : buttonStartColor;

                isStartColor = !isStartColor;
                timer = 0f; // Reset the timer
            }
        }
    }
}



