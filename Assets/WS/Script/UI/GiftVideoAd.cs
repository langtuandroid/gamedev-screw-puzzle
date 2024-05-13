using UnityEngine;
using UnityEngine.UI;
using WS.Script.GameManagers;
using Zenject;

namespace WS.Script.UI
{
    //Will be deleted may be
    public class GiftVideoAd : MonoBehaviour
    {
        [Inject] private SoundManager _soundManager;
        public Text timeCounterTxt;
        public GameObject rewardObj;
        public GameObject counterObj;
        public Button button;
        public Animator anim;
        private bool allowShow = true;
        private float timePerWatch;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            allowShow = (Time.realtimeSinceStartup - ValueStorage.LastLogin) > timePerWatch;

            button.interactable = allowShow && false;
            if(anim)
                anim.enabled = allowShow;
            rewardObj.SetActive(allowShow);
            counterObj.SetActive(!allowShow);
        
            if (!allowShow)
                timeCounterTxt.text = ((int)(timePerWatch - (Time.realtimeSinceStartup - ValueStorage.LastLogin)) / 60).ToString("0") + ":" + ((int)(timePerWatch - (Time.realtimeSinceStartup - ValueStorage.LastLogin)) % 60).ToString("00");
        }

        public void WatchVideoAd()
        {
            ValueStorage.LastLogin = Time.realtimeSinceStartup;
            _soundManager.Click();
        }
        
    }
}
