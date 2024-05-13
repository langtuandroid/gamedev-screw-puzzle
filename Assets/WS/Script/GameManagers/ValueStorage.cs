using UnityEngine;

namespace WS.Script.GameManagers
{
    public class ValueStorage : MonoBehaviour 
    {
        public static bool IsAutoPlay = false;
        public static bool IsSound = true;
        public static float LastLogin = -999;
        
        public static int CoinsData{ 
            get => PlayerPrefs.GetInt ("Coins", 1000);
            set => PlayerPrefs.SetInt ("Coins", value);
        }
        
        public static string PlayerName
        {       
            get => PlayerPrefs.GetString("PlayerName", "xxx");
            set => PlayerPrefs.SetString("PlayerName", value);
        }

        public static int BestResult
        {
            set => PlayerPrefs.SetInt("BestTimerMode", value);
            get => PlayerPrefs.GetInt("BestTimerMode", 0);
        }

        public static int WeaponEquipped
        {
            set => PlayerPrefs.SetInt("PickedWeapon", value);
            get => PlayerPrefs.GetInt("PickedWeapon", 0);
        }
    }
}
