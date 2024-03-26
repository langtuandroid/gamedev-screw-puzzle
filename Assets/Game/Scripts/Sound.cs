using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts
{
	[System.Serializable]
	public class Sound 
	{
		[FormerlySerializedAs("name")] [SerializeField] private string _name;
		[FormerlySerializedAs("clip")] [SerializeField] private AudioClip _clip;
		[FormerlySerializedAs("volume")] [Range(0f, 1f)][SerializeField] private float _volume = .75f;
		[FormerlySerializedAs("volumeVariance")] [Range(0f, 1f)][SerializeField] private float _volumeVariance = .1f;
		[FormerlySerializedAs("pitch")] [Range(.1f, 3f)][SerializeField] private float _pitch = 1f;
		[FormerlySerializedAs("pitchVariance")] [Range(0f, 1f)][SerializeField] private float _pitchVariance = .1f;
		[FormerlySerializedAs("loop")] [SerializeField] private bool _loop;

		public string Name => _name;
		public float Volume => _volume;
		public AudioClip AudioClip => _clip;
		public float Pitch => _pitch;
		public float PitchVariance => _pitchVariance;
		public bool IsLoop => _loop;
		public float VolumeVariance => _volumeVariance;
		
		public AudioSource AudioSource { get; set; }

	}
}
