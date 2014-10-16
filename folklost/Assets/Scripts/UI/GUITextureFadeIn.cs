using UnityEngine;
using System.Collections;

public class GUITextureFadeIn : MonoBehaviour {
	
	public GUITexture m_texture;
	public float m_fadeTime;
	
	private float m_time = 0;
	
	void Awake() {
		if(m_fadeTime <= 0) {
			Debug.LogWarning("Fade time set to a number <= 0");
		}
	}
	
	void Update() {
		m_time += Time.deltaTime;
		if(m_time > m_fadeTime) {
			Color color = m_texture.color;
			color.a = 1;
			m_texture.color = color;
			m_time = 0;
			this.enabled = false;
		} else {
			float alpha = 1 - (Mathf.Abs(m_time - m_fadeTime) / m_fadeTime);
			Color color = m_texture.color;
			color.a = alpha;
			m_texture.color = color;
		}
	}
}
