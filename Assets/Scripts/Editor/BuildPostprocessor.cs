using Integration;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using UnityEngine;


internal static class BuildPostprocessor 
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
         PurchaseIDHolder instancePurchase = PurchaseIDHolder.LoadInstance();
         GDPRLinksHolder instanceGDPRLinks = GDPRLinksHolder.LoadInstance();
         AdMobSettings instanceAdMobSettings = AdMobSettings.LoadInstance();

         if (instancePurchase.IsProduction)
         {
             if (string.IsNullOrEmpty(instancePurchase.SubscriptionMonthID) ||
                 string.IsNullOrEmpty(instancePurchase.SubscriptionYearID) ||
                 string.IsNullOrEmpty(instancePurchase.SubscriptionForeverID) ||
                 string.IsNullOrEmpty(instancePurchase.Buy100Id) ||
                 string.IsNullOrEmpty(instancePurchase.Buy300Id) ||
                 string.IsNullOrEmpty(instancePurchase.Buy1000Id) ||
                 string.IsNullOrEmpty(instancePurchase.Buy3000Id))
             {
                 NotifyBuildFailure(instancePurchase, "Purchase ID",
                     $"Production Purchase ID {instancePurchase} is empty. Please enter a valid Production ID!");
                 throw new BuildFailedException("Error: Not all Production Purchase ID are filled!");
             }
         }
         
         if (instanceGDPRLinks.IsProduction)
         {
             if (string.IsNullOrEmpty(instanceGDPRLinks.PrivacyPolicy) || string.IsNullOrEmpty(instanceGDPRLinks.TermsOfUse))
             {
                 NotifyBuildFailure(instanceGDPRLinks, "GDPRLink",
                     $"Production GDPRLink {instanceGDPRLinks} is empty. Please enter a valid GDPRLink!");
                 throw new BuildFailedException("Error: Not all GDPRLink are filled!");
             }
         }

         if (instanceAdMobSettings.IsProduction)
         {
             if (string.IsNullOrEmpty(instanceAdMobSettings.BannerID) ||
                 string.IsNullOrEmpty(instanceAdMobSettings.InterstitialID) ||
                 string.IsNullOrEmpty(instanceAdMobSettings.RewardedID))
             {
                 NotifyBuildFailure(instanceAdMobSettings, "AdMob ID",
                     $"Production AdMobSettings {instanceAdMobSettings} is empty. Please enter a valid AdMobSettings ID!");
                 throw new BuildFailedException("Error: Not all AdMobSettings ID are filled!");
             }
         }
    }
    
    private static void NotifyBuildFailure<T>(T instance,string title, string message) where T : ScriptableObject
    {
        string dialogTitle = title;
        string dialogMessage = "Error: " + message;
        bool openSettings = EditorUtility.DisplayDialog(dialogTitle, dialogMessage, "Open Scriptable", "Close");
        if (openSettings)
        {
            Selection.activeObject = instance;
            EditorGUIUtility.PingObject(instance); 
        }
    }
}
