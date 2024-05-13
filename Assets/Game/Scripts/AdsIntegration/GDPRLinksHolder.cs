#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

    [CreateAssetMenu(fileName = "GDPRLinks", menuName = "Subscription/GDPRLinks", order = 1)]
    public class GDPRLinksHolder : ScriptableObject
    {
#if UNITY_ANDROID
        [Header("Production Links Android")]
        [SerializeField] private string _privacy;
        [SerializeField] private string _terms;
        
#elif UNITY_IOS
        [Header("Production Links IOS")]
        [SerializeField] private string _privacy;
        [SerializeField] private string _terms;
#endif
        
        [Header("Test Link to Google")]
        [SerializeField] private string _privacyTest = "https://www.google.com";
        [SerializeField] private string _termsTest = "https://www.google.com";
        
        [SerializeField]
        private bool _isProduction;
        public bool IsProduction => _isProduction;

        public string PrivacyPolicy => IsProduction ? _privacy : _privacyTest;
        public string TermsOfUse => IsProduction ? _terms : _termsTest;

        private const string MobileGDPRLinksFile = "GDPRLinks";

        public static GDPRLinksHolder LoadInstance()
        {
            var instance = Resources.Load<GDPRLinksHolder>(MobileGDPRLinksFile);
            Debug.Log("instance = " + instance.name);
            return instance;
        }
    }

