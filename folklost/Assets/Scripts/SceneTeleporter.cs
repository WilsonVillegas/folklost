using UnityEngine;
using System.Collections;
using RenPy;

public class SceneTeleporter : MonoBehaviour {
	
	public string m_scene;
	
	public bool m_fade = false;
	public float m_fadeTime = 3;
	public Color m_fadeColor;
	
	private Texture2D m_tex;
	private float m_time;
	private bool started = false;
	
	void Start() {
		m_tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		m_tex.SetPixel(0, 0, m_fadeColor);
		m_tex.Apply();
	}
	
	void Update() {
		if(Static.Variables.ContainsKey("cave") && !started)
		{
			StartCoroutine(GoToLevel());
		}
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), m_tex);
	}
	
	private IEnumerator GoToLevel() {
		GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		
		while(m_time <= 3) {
			yield return new WaitForEndOfFrame();
			m_time += Time.deltaTime;
			float ratio = m_time/m_fadeTime;
			audio.Play();
			m_fadeColor.a = Mathf.Clamp(ratio*1.05f, 0, 1);
			m_tex.SetPixel(0, 0, m_fadeColor);
			m_tex.Apply();

		}
		
		Application.LoadLevel(m_scene);
	}
}
