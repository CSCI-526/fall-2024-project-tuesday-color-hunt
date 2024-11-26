using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCollider : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject demo;
    private PlayerMovement pm;
    [SerializeField] private GameObject helpText;

    private bool enteredOnce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enteredOnce)
        {
            if (collision.CompareTag("Player"))
            {
                pm = FindObjectOfType<PlayerMovement>();
                Time.timeScale = 0f;
                Cursor.visible = true;
                pm.enabled = false;
                videoPlayer.isLooping = true;
                helpText.SetActive(false);
                demo.SetActive(true);
                videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "demoUnity.mp4");
                videoPlayer.loopPointReached += OnVideoEnd;

                enteredOnce = true;
            }
            
        }
        
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.time = 0; // Reset video to start
        vp.Play();   // Play again
    }

    public void EndVideo()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        demo.SetActive(false);
        helpText.SetActive(true);
        pm.enabled = true;
    }

    public void SkipButton()
    {
        EndVideo();
    }
}
