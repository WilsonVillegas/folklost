using UnityEngine;
using System.Collections;

public class PlayEnding : MonoBehaviour {
	
	public bool m_fade = false;
	public float delayTime;
	public float m_fadeTime = 5;
	public Color m_fadeColor;
	public AudioSource[] m_fadeAudio;
	
	private Texture2D m_tex;
	private float m_time;
	
	void Start () {
		StartCoroutine(MyLoadLevel(delayTime,5));
		m_tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		m_tex.SetPixel(0, 0, m_fadeColor);
		m_tex.Apply();
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), m_tex);
	}
	
	IEnumerator MyLoadLevel(float delay, int level)
	{
		yield return new WaitForSeconds(delay);
		
		float[] origVol = new float[m_fadeAudio.Length];
		for(int i = 0; i < m_fadeAudio.Length; i++) {
			origVol[i] = m_fadeAudio[i].volume;
		}
		
		while(m_time <= m_fadeTime) {
			yield return new WaitForEndOfFrame();
			m_time += Time.deltaTime;
			float ratio = m_time/m_fadeTime;
			
			m_fadeColor.a = Mathf.Clamp(ratio*1.05f, 0, 1);
			m_tex.SetPixel(0, 0, m_fadeColor);
			m_tex.Apply();
			
			for(int i = 0; i < m_fadeAudio.Length; i++) {
				m_fadeAudio[i].volume = origVol[i] - ratio*origVol[i];
			}
		}
		
		Application.LoadLevel("EndGame");
	}
	
	

}
