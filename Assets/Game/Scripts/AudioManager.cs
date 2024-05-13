using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts
{
	public class AudioManager : MonoBehaviour
	{

		[FormerlySerializedAs("sounds")] [SerializeField] private Sound[] _sounds;
		void Awake()
		{
			foreach (Sound sound in _sounds)
			{
				sound.AudioSource = gameObject.AddComponent<AudioSource>();
				sound.AudioSource.clip = sound.AudioClip;
				sound.AudioSource.loop = sound.IsLoop;
			}
		}
		

		public void Play(string soundName)
		{
			Sound sound = Array.Find(_sounds, item => item.Name == soundName);
			if (sound == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}

			sound.AudioSource.volume = sound.Volume * (1f + UnityEngine.Random.Range(-sound.VolumeVariance / 2f, sound.VolumeVariance / 2f));
			sound.AudioSource.pitch = sound.Pitch * (1f + UnityEngine.Random.Range(-sound.VolumeVariance / 2f, sound.VolumeVariance / 2f));

			sound.AudioSource.Play();
		}
	}
}
