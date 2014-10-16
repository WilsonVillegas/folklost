using UnityEngine;
using System.Collections.Generic;

public class MoodZone : MonoBehaviour {
	
	public float m_fadeout;
	public Color m_fogColor = new Color(53f/255f,53f/255f,53f/255f,1);
	public float m_fogDensity = 0.03f;
	public AudioSource[] muteSources;
	public AudioSource[] unmuteSources;
	
	private FogCoroutine fogCoroutine;
	private static GameObject fogCoroutineObject;
	private MuteCoroutine[] muteCoroutines;
	private MuteCoroutine[] unmuteCoroutines;
	
	void Awake() {
		List<MuteCoroutine> mutecr = new List<MuteCoroutine>();
		foreach(AudioSource source in muteSources) {
			MuteCoroutine mc = source.gameObject.AddComponent<MuteCoroutine>();
			mc.Mute = true;
			mutecr.Add(mc);
		}
		muteCoroutines = mutecr.ToArray();
		
		List<MuteCoroutine> unmutecr = new List<MuteCoroutine>();
		foreach(AudioSource source in unmuteSources) {
			MuteCoroutine mc = source.gameObject.AddComponent<MuteCoroutine>();
			mc.Mute = false;
			unmutecr.Add(mc);
		}
		unmuteCoroutines = unmutecr.ToArray();
		
		if(fogCoroutineObject == null) {
			fogCoroutineObject = new GameObject("Fog Coroutines");
		}
		fogCoroutine = fogCoroutineObject.AddComponent<FogCoroutine>();
		fogCoroutine.Color = m_fogColor;
		fogCoroutine.Density = m_fogDensity;
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			UpdateSources(gameObject.GetHashCode(), true);
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
			UpdateSources(gameObject.GetHashCode(), false);
		}
	}
	
	private void UpdateSources(int hash, bool onEnter) {
		foreach(MuteCoroutine mc in muteCoroutines) {
			mc.Trigger(hash, onEnter, m_fadeout);
		}
		foreach(MuteCoroutine mc in unmuteCoroutines) {
			mc.Trigger(hash, onEnter, m_fadeout);
		}
		
		fogCoroutine.Trigger(hash, onEnter, m_fadeout);
	}
}
