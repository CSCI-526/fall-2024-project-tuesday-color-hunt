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
    public void SelectTutorial_5()
    {
        SceneManager.LoadScene("Tutorial-5");
    }
    public void SelectTutorial_6()
    {
        SceneManager.LoadScene("Tutorial-6");
    }

    public void SelectMedium_1()
    {
        SceneManager.LoadScene("Medium-1");
    }


    public void SelectAbandonedLevel_1()
    {
        SceneManager.LoadScene("Abandoned-1");
    }
    

    
}
