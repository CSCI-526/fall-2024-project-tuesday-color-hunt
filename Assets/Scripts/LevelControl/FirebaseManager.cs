using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Proyecto26;
using System.IO;
using System;

public class FirebaseManager : MonoBehaviour
{
    public bool levelCleared;
    private double levelStartTime;
    private int deathCount;
    private int lightToggleCount;
    private int lightMixedCount;

    private const string DatabaseUrl = "https://project-6115520849454422510-default-rtdb.firebaseio.com/";
    private string authToken;

    private void Awake()
    {
        levelStartTime = Time.time;

       // deathCount = PlayerPrefs.GetInt("DeathCount", 0);
       // lightToggleCount = PlayerPrefs.GetInt("lightToggleCount", 0);
    }

    public void SendDataToDatabase(double time)
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        // Create a user object with the data to send
        User user = new User(currentLevel, time, deathCount, lightToggleCount, lightMixedCount);

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
            deathCount = PlayerPrefs.GetInt("DeathCount", 0);
            lightToggleCount = PlayerPrefs.GetInt("lightToggleCount", 0);
            lightMixedCount = PlayerPrefs.GetInt("lightMixedCount", 0);

            levelCleared = !levelCleared;
            double timeSpentOnLevel = Time.time - levelStartTime;
            SendDataToDatabase(timeSpentOnLevel);
            levelStartTime = Time.time;

            // Reset deathCount after data is sent
            deathCount = 0;
            PlayerPrefs.SetInt("DeathCount", deathCount);
            PlayerPrefs.Save();

            lightToggleCount = 0;
            PlayerPrefs.SetInt("lightToggleCount", lightToggleCount);
            PlayerPrefs.Save();

            lightMixedCount = 0;
            PlayerPrefs.SetInt("lightMixedCount", lightMixedCount);
            PlayerPrefs.Save();
        }
    }

    [System.Serializable]
    public class User
    {
        public string the_level_is;
        public double time_spent_to_clear_the_level_is;
        public int times_of_death;
        public int times_of_light_toggled;
        public int times_of_light_mixed;

        public User(string currentLevel, double time, int death, int light, int lightMixed)
        {
            this.the_level_is = currentLevel;
            this.time_spent_to_clear_the_level_is = time;
            this.times_of_death = death;
            this.times_of_light_toggled = light;
            this.times_of_light_mixed = lightMixed;
        }
    }
}
