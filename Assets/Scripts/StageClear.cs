using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class StageClear : MonoBehaviour
{
    public GameObject stageClearCanvas;
    public PlayerMovement player;
    FirebaseManager fm;

    void Start()
    {
        fm = FindObjectOfType<FirebaseManager>();
    }

    public void ShowStageClearUI()
    {
        fm.levelCleared = true;

        stageClearCanvas.SetActive(true);
        Cursor.visible = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.canMove = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevelButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowStageClearUI();
        }
    }
}
