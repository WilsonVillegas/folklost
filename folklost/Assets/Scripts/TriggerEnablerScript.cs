using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerEnablerScript : MonoBehaviour {
	
	/// <summary>
	/// True if we only want to change the state of the objects when the
	/// trigger is entered. If false, the objects are only enabled when
	/// the player is in the trigger area.
	/// </summary>
	public bool onlyOnEnter;

	/// <summary>
	/// Whether or not to trigger onEnable
	/// </summary>
	public bool m_onEnable;
	
	public List<GameObject> m_gameObjects;
	public List<MonoBehaviour> m_behaviours;
	public List<Light> m_lights;
	public List<AudioSource> m_audio;
	
	private bool m_on;
	
	void Update () {
		foreach(GameObject go in m_gameObjects) {
			go.SetActive(m_on);
		}
		foreach(MonoBehaviour b in m_behaviours) {
			b.enabled = m_on;
		}
		foreach(Light l in m_lights) {
			l.enabled = m_on;
		}
	}
	
	void OnTriggerEnter(Collider col) {
		if(this.enabled && !m_on && col.tag == "Player") {
			m_on = true;
			foreach(AudioSource a in m_audio) {
				a.Play();
			}
		}
	}
	
	void OnTriggerExit(Collider col) {
		if(this.enabled && col.tag == "Player" && !onlyOnEnter) {
			m_on = false;
		}
	}

	void OnEnable() {
		if(m_onEnable) {
			if(this.enabled && !m_on) {
				m_on = true;
				foreach(AudioSource a in m_audio) {
					a.Play();
				}
			}
		}
	}
}
