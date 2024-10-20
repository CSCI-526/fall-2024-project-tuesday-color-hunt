using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Ensure you're using TextMeshPro

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;  
    public float timeSpent = 0f;
    public bool isTiming = true;

    void Update()
    {
        if (isTiming)
        {
            timeSpent += Time.deltaTime;
            timerText.text = FormatTime(timeSpent);  
        }
    }

    // Method to format time in MM:SS:MS
    public string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F); 
        int seconds = Mathf.FloorToInt(time % 60F);  
        int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);  

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);  
    }
    public void StopTimer()
    {
        isTiming = false;
    }
}
