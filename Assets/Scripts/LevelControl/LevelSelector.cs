using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    public void SelectTutorial_1()
    {
        SceneManager.LoadScene("Tutorial-1");
    }
    public void SelectTutorial_2()
    {
        SceneManager.LoadScene("Tutorial-2");
    }
    public void SelectTutorial_3()
    {
        SceneManager.LoadScene("Tutorial-3");
    }
    public void SelectTutorial_4()
    {
        SceneManager.LoadScene("Tutorial-4");
    }

    public void SelectLevel_1()
    {
        SceneManager.LoadScene("Level-1");
    }
    public void SelectLevel_2()
    {
        SceneManager.LoadScene("Level-2");
    }
    public void SelectLevel_3()
    {
        SceneManager.LoadScene("Level-3");
    }
    public void SelectLevel_4()
    {
        SceneManager.LoadScene("Level-4");
    }
}
