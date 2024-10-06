using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    private Camera mainCamera;
    private List<GameObject> objectsToBeDisappered;
    [SerializeField] public StageClear sc;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.backgroundColor = Color.black;

        objectsToBeDisappered = new List<GameObject>();

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        objectsToBeDisappered.AddRange(walls);

        // Find all GameObjects with the tag "Ground" and add them to the list
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
        objectsToBeDisappered.AddRange(grounds);

    }

    // Update is called once per frame
    void Update()
    {

        Color backgroundColor = mainCamera.backgroundColor;

        // Convert the color to RGB values
        float r = backgroundColor.r * 255;
        float g = backgroundColor.g * 255;
        float b = backgroundColor.b * 255;


        // Print the RGB values
        print("Main Camera Background Color (RGB): " + r + ", " + g + ", " + b);

        if (CompareColors(backgroundColor, 255, 255, 255))
        {
            sc.ShowStageClearUI();
        }

        foreach (GameObject obj in objectsToBeDisappered)
        {
            SpriteRenderer objSprite = obj.GetComponent<SpriteRenderer>();
            if (CompareColors(mainCamera.backgroundColor, objSprite.color))
            {
                obj.SetActive(false);
                print("Obstacle Name: " + obj.name + "with color: " + objSprite.color);
            }
            else
            {
                obj.SetActive(true);
            }
        }

        /*
        if (CompareColorWithRGB(mainCamera.backgroundColor, 100, 100, 100))
        }
            SceneManager.LoadScene("Level2");
        }*/

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public bool CompareColors(Color color1, Color color2)
    {
        // Compare the colors with a small tolerance to account for floating-point precision issues
        return Mathf.Approximately(color1.r, color2.r) &&
               Mathf.Approximately(color1.g, color2.g) &&
               Mathf.Approximately(color1.b, color2.b);
    }

    public bool CompareColors(Color color, float r, float g, float b)
    {
        // Normalize the RGB values (from 0-255 scale to 0-1 scale)
        Color comparisonColor = new Color(r / 255f, g / 255f, b / 255f);

        // Allow a small tolerance to account for floating-point imprecision
        const float tolerance = 0.01f;
        return Mathf.Abs(color.r - comparisonColor.r) < tolerance &&
               Mathf.Abs(color.g - comparisonColor.g) < tolerance &&
               Mathf.Abs(color.b - comparisonColor.b) < tolerance;
    }

    public void AddColor(float r, float g, float b)
    {
        // Get the current background color in normalized values (0-1)
        Color currentColor = mainCamera.backgroundColor;

        // Convert current color from normalized to RGB (0-255)
        float currentR = currentColor.r * 255f;
        float currentG = currentColor.g * 255f;
        float currentB = currentColor.b * 255f;

        // Add the new RGB values to the current RGB values
        float newR = currentR + r;
        float newG = currentG + g;
        float newB = currentB + b;

        // Clamp the new values to ensure they are within the valid range (0-255)
        newR = Mathf.Clamp(newR, 0, 255);
        newG = Mathf.Clamp(newG, 0, 255);
        newB = Mathf.Clamp(newB, 0, 255);

        // Convert the result back to normalized values (0-1)
        float normalizedR = newR / 255f;
        float normalizedG = newG / 255f;
        float normalizedB = newB / 255f;

        // Set the new mixed color as the camera's background color
        mainCamera.backgroundColor = new Color(normalizedR, normalizedG, normalizedB);
    }

    public void MinusColor(float r, float g, float b)
    {
        // Get the current background color in normalized values (0-1)
        Color currentColor = mainCamera.backgroundColor;

        // Convert current color from normalized to RGB (0-255)
        float currentR = currentColor.r * 255f;
        float currentG = currentColor.g * 255f;
        float currentB = currentColor.b * 255f;

        // Subtract the new RGB values from the current RGB values
        float newR = currentR - r;
        float newG = currentG - g;
        float newB = currentB - b;

        // Clamp the new values to ensure they are within the valid range (0-255)
        newR = Mathf.Clamp(newR, 0, 255);
        newG = Mathf.Clamp(newG, 0, 255);
        newB = Mathf.Clamp(newB, 0, 255);

        // Convert the result back to normalized values (0-1)
        float normalizedR = newR / 255f;
        float normalizedG = newG / 255f;
        float normalizedB = newB / 255f;

        // Set the new mixed color as the camera's background color
        mainCamera.backgroundColor = new Color(normalizedR, normalizedG, normalizedB);
    }
}
