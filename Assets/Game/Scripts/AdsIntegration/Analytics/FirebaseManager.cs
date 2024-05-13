using UnityEngine;
using Firebase.Extensions;
using Firebase;
using Firebase.Analytics;

public class FirebaseManager : MonoBehaviour
{
  private FirebaseApp app;

  private void Awake()
  {
    CreateFireBase();
    ConfirmGooglePlayServices();
  }

  private void CreateFireBase()
  {
    app = FirebaseApp.Create();
  }

  private void SendFirebaseEvent()
  {
    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
  }

  private void ConfirmGooglePlayServices()
  {
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    {
      var dependencyStatus = task.Result;
      if (dependencyStatus == DependencyStatus.Available)
      {
        // Create and hold a reference to your FirebaseApp,
        // where app is a Firebase.FirebaseApp property of your application class.
        app = FirebaseApp.DefaultInstance;

        // Set a flag here to indicate whether Firebase is ready to use by your app.
      }
      else
      {
        Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
        // Firebase Unity SDK is not safe to use here.
      }
    });
    SendFirebaseEvent();
  }
}