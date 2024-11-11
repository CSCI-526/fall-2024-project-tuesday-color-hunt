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
    public double levelStartTime;

    private const string DatabaseUrl = "https://project-6115520849454422510-default-rtdb.firebaseio.com/";
    private string authToken;

    private void Awake()
    {
        levelStartTime = Time.time;
    }

    public void SendDataToDatabase(int restart, double time, int deathCount, int lightToggleCount, int lightMixedCount)
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        // Create a user object with the data to send
        User user = new User(currentLevel, restart, time, deathCount, lightToggleCount, lightMixedCount);

        // Get the current date and time and format it as a string for the user key
        string dateTimeNow = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        // Create the URL with the current date and time
        string url = $"{DatabaseUrl}ForBetaTesting/{dateTimeNow}.json?auth={authToken}";

        RestClient.Put(url, user);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelCleared)
        {
            int restartCount = PlayerPrefs.GetInt("RestartCount", 0);
            int deathCount = PlayerPrefs.GetInt("DeathCount", 0);
            int lightToggleCount = PlayerPrefs.GetInt("lightToggleCount", 0);
            int lightMixedCount = PlayerPrefs.GetInt("lightMixedCount", 0);
            
            levelCleared = !levelCleared;
            double timeSpentOnLevel = Time.time - levelStartTime;
            string savedString = PlayerPrefs.GetString("TimeSpent", "0");
            double savedTimeSpent = double.Parse(savedString);
            SendDataToDatabase(restartCount, timeSpentOnLevel + savedTimeSpent, deathCount, lightToggleCount, lightMixedCount);

            // Reset after data is sent
            PlayerPrefs.SetInt("DeathCount", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("lightToggleCount", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("lightMixedCount", 0);
            PlayerPrefs.Save();

            double zeroTime = 0.0;
            PlayerPrefs.SetString("TimeSpent", zeroTime.ToString());
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("RestartCount", 0);
            PlayerPrefs.Save();
        }
    }

    [System.Serializable]
    public class User
    {
        public string the_level_is;
        public double time_spent;
        public int times_of_death;
        public int times_of_light_toggled;
        public int times_of_light_mixed;
        public int times_of_restart;

        public User(string currentLevel, int restart, double time, int death, int light, int lightMixed)
        {
            this.the_level_is = currentLevel;
            this.times_of_restart = restart;
            this.time_spent = time;
            this.times_of_death = death;
            this.times_of_light_toggled = light;
            this.times_of_light_mixed = lightMixed;

        }
    }
}
