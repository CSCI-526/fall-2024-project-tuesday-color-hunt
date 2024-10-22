using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using Proyecto26;
using System.IO;
using System;

public class FirebaseManager : MonoBehaviour
{
    public bool levelCleared;
    private double levelStartTime;

    private const string DatabaseUrl = "https://project-6115520849454422510-default-rtdb.firebaseio.com/";
    private string authToken;

    private void Awake()
    {
        levelStartTime = Time.time;
    }


    public void SendDataToDatabase(double time)
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        // Create a user object with the data to send
        User user = new User(time, currentLevel);

        // Get the current date and time and format it as a string for the user key
        string dateTimeNow = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        // Create the URL with the current date and time
        string url = $"{DatabaseUrl}users/{dateTimeNow}.json?auth={authToken}";

        RestClient.Put(url, user);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelCleared)
        {
            levelCleared = !levelCleared;
            double timeSpentOnLevel = Time.time - levelStartTime;
            SendDataToDatabase(timeSpentOnLevel);
            levelStartTime = Time.time;
        }
    }

    [System.Serializable]
    public class User
    {
        public double time_spent_to_clear_the_level_is;
        public string the_level_is;

        public User(double time, string currentLevel)
        {
            this.time_spent_to_clear_the_level_is = time;
            this.the_level_is = currentLevel;
        }
    }
}
