using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject stageClearMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject helpText;

    private PlayerMovement pm;
    private FirebaseManager fm;
    public bool isPaused;
    public bool isCleared;
    private int pageNum;

    void Start()
    {
        fm = FindObjectOfType<FirebaseManager>();
        pm = FindObjectOfType<PlayerMovement>();
        Time.timeScale = 1f;
        //pm.enabled = false;
        helpText.SetActive(true);
    }

    void Update()
    {
        if (!isCleared)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }

            if (isPaused)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    pageNum--;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    pageNum++;
                }

                if (pageNum == 0)
                {
                    pauseMenu.transform.Find("LightTips").gameObject.SetActive(false);
                    pauseMenu.transform.Find("KeybindTips").gameObject.SetActive(true);
                    pauseMenu.transform.Find("SelectionMenu").gameObject.SetActive(false);
                }
                else if (pageNum == 1)
                {
                    pauseMenu.transform.Find("LightTips").gameObject.SetActive(true);
                    pauseMenu.transform.Find("KeybindTips").gameObject.SetActive(false);
                    pauseMenu.transform.Find("SelectionMenu").gameObject.SetActive(false);
                }
                else if (pageNum == 2)
                {
                    pauseMenu.transform.Find("LightTips").gameObject.SetActive(false);
                    pauseMenu.transform.Find("KeybindTips").gameObject.SetActive(false);
                    pauseMenu.transform.Find("SelectionMenu").gameObject.SetActive(true);
                }
            }

            if (pageNum > 1)
            {
                pageNum = 2;
            }
            else if (pageNum < 1)
            {
                pageNum = 0;
            }
        }
        
    }

    public void ShowStageClearUI()
    {
        isCleared = true;
        fm.levelCleared = true;
        stageClearMenu.SetActive(true);
        helpText.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0f;
        pm.enabled = false;
    }

    public void PauseGame()
    {
        pageNum = 0;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        isPaused = true;
        pm.enabled = false;
        helpText.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        isPaused = false;
        pm.enabled = true;
        helpText.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void LoadNextLevelButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
