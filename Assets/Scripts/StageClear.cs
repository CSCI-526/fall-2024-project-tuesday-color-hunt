using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
    public GameObject stageClearCanvas;
    public PlayerMovement player;

    public void ShowStageClearUI()
    {
        stageClearCanvas.SetActive(true);
        Cursor.visible = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.canMove = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowStageClearUI();
        }
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
