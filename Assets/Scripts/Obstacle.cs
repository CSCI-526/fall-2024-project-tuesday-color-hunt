using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float uprightThreshold = 0.9f; // Close to 1 means almost vertical
    public float horizontalThreshold = 0.5f; // Lower means close to horizontal

    void Update()
    {
        // Calculate the dot product between the object's up vector and the world's up vector
        float alignment = Vector3.Dot(transform.up, Vector3.up);

        // Change the object's layer based on its orientation
        if (alignment >= uprightThreshold)
        {
            ChangeLayer("Wall");
        }
        else if (alignment <= horizontalThreshold)
        {
            ChangeLayer("Ground");
        }
    }

    // Helper function to change the layer by name
    private void ChangeLayer(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        gameObject.layer = layer;
    }
}
