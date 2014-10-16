using UnityEngine;
using System.Collections;

public class DemoEnding : MonoBehaviour {
	
	public string m_variable;
	private float delayTime = 7;
	private float m_fadeTime = 5;
	public Color m_fadeColor;
	public Font m_font;
	public Texture2D restart;
	
	private Texture2D m_tex;
	private float m_time;
	private bool triggered;
	private bool done;
	private GameObject player;
	
	void Start () {
		m_tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		m_tex.SetPixel(0, 0, m_fadeColor);
		m_tex.Apply();
		player = GameObject.Find("Player");
	}

	void Update() {
		if(!triggered && RenPy.Static.Variables.ContainsKey(m_variable) && RenPy.Static.Variables[m_variable] == "True") {
			StartCoroutine(ShowGameOver(delayTime,5));
		}
		
		if(done) {
			Screen.lockCursor = false;
			
			if(Input.GetKey(KeyCode.Quote)) {
				player.SetActive(true);
				Destroy(this.gameObject);
			} else {
				player.SetActive(false);
			}
		}
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), m_tex);
		
		if(done) {
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = Color.white;
			style.fontSize = UIStatic.FontSize*2;
			style.wordWrap = true;
			style.font = m_font;
			
			string str = "Thanks for playing our E3 Demo. \n Vote for us on Steam Greenlight!";
			Rect rect = new Rect(0,0,0,0);
			rect.width = Mathf.Min(style.CalcSize(new GUIContent(str)).x + UIStatic.FontSize, Screen.width - UIStatic.FontSize*2);
			rect.height =  style.CalcHeight(new GUIContent(str), rect.width) + UIStatic.FontSize;
			rect.x = Screen.width/2 - rect.width/2;
			//rect.y = UIStatic.FontSize*3;
			rect.y = Screen.height/3;

			// Render the speech
			GUI.Label(rect, str, style);
			if(GUI.Button(new Rect(6.5f*Screen.width/14f,Screen.height-Screen.height/3,Screen.width/7f,50),  restart, GUIStyle.none)) {
				Application.LoadLevel("Menu");
			}
		}
	}
	
	IEnumerator ShowGameOver(float delay, int level)
	{
		yield return new WaitForSeconds(delay);
		
		triggered = true;
		
		while(m_time <= m_fadeTime) {
			yield return new WaitForEndOfFrame();
			m_time += Time.deltaTime;
			float ratio = m_time/m_fadeTime;
			
			m_fadeColor.a = Mathf.Clamp(ratio*1.05f, 0, 1);
			m_tex.SetPixel(0, 0, m_fadeColor);
			m_tex.Apply();
		}
		
		done = true;
	}
}
