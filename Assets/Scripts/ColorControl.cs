using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControl : MonoBehaviour
{

    public bool CompareColors(Color color1, Color color2)
    {
        // Compare the colors with a small tolerance to account for floating-point precision issues
        return Mathf.Approximately(color1.r, color2.r) &&
               Mathf.Approximately(color1.g, color2.g) &&
               Mathf.Approximately(color1.b, color2.b);
    }

    public Color AddColor(Color color1, Color color2)
    {
        // Directly add colors and clamp to 0-1 range
        float newR = Mathf.Clamp01(color1.r + color2.r);
        float newG = Mathf.Clamp01(color1.g + color2.g);
        float newB = Mathf.Clamp01(color1.b + color2.b);
        return new Color(newR, newG, newB);
    }

    public Color MinusColor(Color color1, Color color2)
    {
        // Directly subtract colors and clamp to 0-1 range
        float newR = Mathf.Clamp01(color1.r - color2.r);
        float newG = Mathf.Clamp01(color1.g - color2.g);
        float newB = Mathf.Clamp01(color1.b - color2.b);
        return new Color(newR, newG, newB);
    }

    public Color AddColor(Color color, float r, float g, float b)
    {
        // Convert from 0-255 to 0-1 and add the colors
        Color colorToAdd = new Color(r / 255f, g / 255f, b / 255f);
        return AddColor(color, colorToAdd);
    }

    public Color MinusColor(Color color, float r, float g, float b)
    {
        // Convert from 0-255 to 0-1 and subtract the colors
        Color colorToSubtract = new Color(r / 255f, g / 255f, b / 255f);
        return MinusColor(color, colorToSubtract);
    }
}


