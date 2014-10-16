using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightFadeout : MonoBehaviour {

	public Light[] m_lights;
	public float m_time = 3f;
	
	private Dictionary<Light, float> m_originalValues;
	private Dictionary<Light, float> m_currentValues;
	
	void Awake() {
		m_originalValues = new Dictionary<Light,float>();
		m_currentValues = new Dictionary<Light,float>();
		foreach(Light l in m_lights) {
			m_originalValues.Add(l, l.intensity);
			m_currentValues.Add(l, l.intensity);
		}
	}
	
	void Start() {
		StartCoroutine(Fade());
	}
	
	void OnDestroy() {
		SetIntensity(m_originalValues);
	}
	
	private IEnumerator Fade() {
		float waited = 0;
		while(waited < m_time) {
			yield return new WaitForEndOfFrame();
			
			waited += Time.deltaTime;
			foreach(Light l in m_lights) {
				float intensity = Mathf.Lerp(m_originalValues[l], 0, waited/m_time);
				m_currentValues[l] = intensity;
			}
			SetIntensity(m_currentValues);
		}
	}
	
	private void SetIntensity(Dictionary<Light, float> intensities) {
		foreach(Light l in m_lights) {
			l.intensity = intensities[l];
		}
	}
}
