using UnityEngine;
using System.Collections;

/// <summary>
/// Used to store information about footstep sounds that should be played when
/// in contact with this surface.
/// </summary>
public class FootstepsMeta : MonoBehaviour {
	
	public AudioClip[] clips;
	private float minVolume = 0.4f;
	private float maxVolume = 0.6f;
	private float minPitch= 0.85f;
	private float maxPitch = 1.15f;
	
	public void PlayFootstep(AudioSource source) {
		if(clips.Length == 0) {
			return;
		}
		
		source.pitch = Random.Range(minPitch, maxPitch);
		source.volume = Random.Range(minVolume, maxVolume);
		source.clip = clips[Random.Range(0, clips.Length-1)];
		source.Play();
	}
}
