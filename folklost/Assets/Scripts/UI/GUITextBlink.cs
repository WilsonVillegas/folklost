using UnityEngine;
using System.Collections;

public class GUITextBlink : MonoBehaviour {
	
	public GUIText m_text;
	public float m_blinkTime;
	
	private float m_time = 0;
	
	void Awake() {
		if(m_blinkTime <= 0) {
			Debug.LogWarning("Blink time set to a number <= 0");
		}
	}
	
	void Update() {
		m_time += Time.deltaTime;
		if(m_time >= m_blinkTime*2) {
			m_time -= m_blinkTime*2;
		}
		
		float alpha = 1 - Mathf.Abs(m_time - m_blinkTime) / m_blinkTime;
		Color color = m_text.color;
		color.a = alpha;
		m_text.color = color;
	}
}
