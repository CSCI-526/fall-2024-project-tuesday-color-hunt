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
        player.canMove = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
