using UnityEngine;

namespace WS.Script.GameManagers
{
    public class SoundManager : MonoBehaviour
    {
   
        [SerializeField] private AudioClip soundClick;
        
        [Header("GAMEPLAY")]
        public AudioClip ImpactSound;
        [Header("Shop")]
        public AudioClip soundPurchasedItem;
        public AudioClip soundPickItem;
        [Tooltip("Place the sound in this to call it in another script by: SoundManager.PlaySfx(soundname);")]
        
        public AudioClip soundGameover;
        private AudioSource musicAudio;
        private AudioSource soundFx;

        private float SoundVolume => soundFx.volume;
        

        public void PauseMusic(bool isPause)
        {
            if (isPause)
                musicAudio.mute = true;
            else
                musicAudio.mute = false;
     
        }

        public void Click()
        {
            PlaySfx(soundClick, 1);
        }

        private void Awake()
        {
            musicAudio = gameObject.AddComponent<AudioSource>();
            musicAudio.loop = true;
            musicAudio.volume = 0.5f;
            soundFx = gameObject.AddComponent<AudioSource>();
        }

        public void PlaySfx(AudioClip clip)
        {
            PlaySound(clip, soundFx);
        }
        
        public void PlaySfx(AudioClip clip, float volume)
        {
            PlaySound(clip, soundFx, volume);
        }
        

        private void PlaySound(AudioClip clip, AudioSource audioOut)
        {
            if (clip == null)
            {
                return;
            }

            if (audioOut == musicAudio)
            {
                audioOut.clip = clip;
                audioOut.Play();
            }
            else
                audioOut.PlayOneShot(clip, SoundVolume);
        }

        private void PlaySound(AudioClip clip, AudioSource audioOut, float volume)
        {
            if (clip == null)
            {
                return;
            }

            if (audioOut == musicAudio)
            {
                audioOut.clip = clip;
                audioOut.Play();
            }
            else
                audioOut.PlayOneShot(clip, SoundVolume * volume);
        }


    }
}
