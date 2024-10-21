using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine.SceneManagement; // Import SceneManager for scene handling

public class FirebaseManager : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth fAuth;
    public FirebaseUser fUser;
    public DatabaseReference dbReference;
    public bool levelCleared;

    private double levelStartTime;

    private void Awake()
    {
        // Check that all Firebase dependencies are ready
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // If available, initialize Firebase
                InitializeFirebase();
            }
        });

        levelStartTime = Time.time;
    }

    private void InitializeFirebase()
    {
        // Set up Firebase Authentication
        fAuth = FirebaseAuth.DefaultInstance;
        // Set up Firebase Database
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SendDataToDatabase(double time)
    {
        // Generate userId based on current date and time
        string userId = System.DateTime.Now.ToString("yyyyMMddHHmmss");

        // Get the name of the current scene
        string currentLevel = SceneManager.GetActiveScene().name;

        // Create a user object with the data to send
        User user = new User(time, currentLevel);

        // Save data in the database under "Players/userId"
        string jsonData = JsonUtility.ToJson(user);
        dbReference.Child("Players").Child(userId).SetRawJsonValueAsync(jsonData).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                print("Data successfully sent to Firebase with userId: " + userId);
            }
            else
            {
                print("Failed to send data to Firebase: " + task.Exception);
            }
        });
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
