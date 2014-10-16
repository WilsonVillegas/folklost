using UnityEngine;
using System.Collections;

public class UIMenu : MonoBehaviour
{
	public string m_levelName;
	public float m_transitionTime;
	public AudioSource[] fadeAudio;
	private float [] origVol;

	private Texture2D tex;
	private Color color;
	private float m_time;
	private bool triggered;
	
	void Awake() {
		color = new Color(0,0,0,0);
		tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		tex.SetPixel(0, 0, color);
		tex.Apply();
	}
	
	void Start() {
		RenPy.Static.Variables.Clear();
	}
	
	void Update () {
		if(!triggered && Input.GetButtonDown("Interact")) {
			StartCoroutine(GoToLevel());
		}
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), tex);
	}
	
	private IEnumerator GoToLevel() {
		triggered = true;
		origVol = new float[fadeAudio.Length];
		for(int i = 0; i < fadeAudio.Length; i++) {
			origVol[i] = fadeAudio[i].volume;
		}
		
		while(m_time <= m_transitionTime) {
			yield return new WaitForEndOfFrame();
			m_time += Time.deltaTime;
			float ratio = m_time/m_transitionTime;
			
			color.a = Mathf.Clamp(ratio*1.05f, 0, 1);
			tex.SetPixel(0, 0, color);
			tex.Apply();
			
			for(int i = 0; i < fadeAudio.Length; i++) {
				fadeAudio[i].volume = origVol[i] - ratio*origVol[i];
			}
		}
		
		Application.LoadLevel(m_levelName);
	}
}
