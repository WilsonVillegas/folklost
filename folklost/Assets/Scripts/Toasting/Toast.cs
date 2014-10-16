using UnityEngine;
using System.Collections;

public class Toast : MonoBehaviour {
	
	private Texture m_texture;
	public Texture Texture {
		get { return m_texture; }
		set { m_texture = value; }
	}
	
	private string m_message;
	public string Message {
		get { return m_message; }
		set { m_message = value; }
	}
	
	private float m_popupTime;
	public float PopupTime {
		get { return m_popupTime; }
		set { m_popupTime = value; }
	}
	
	private Font m_font;
	public Font ToastFont {
		get { return m_font; }
		set { m_font = value; }
	}

	private int m_fontSize;
	public int FontSize {
		get { return m_fontSize; }
		set { m_fontSize = value; }
	}
	
	private const float m_transitionTime = 0.5f;
	private float m_ratio = 0.0f;
	
	void Start() {
		StartCoroutine(Popup());
	}
	
	IEnumerator Popup() {
		float m_time = 0;
		while(m_time < m_transitionTime) {
			yield return new WaitForEndOfFrame();
			m_ratio = Mathf.Lerp(m_time, m_transitionTime, m_time/m_transitionTime) / m_transitionTime;
			m_time += Time.deltaTime;
		}
		
		yield return new WaitForSeconds(m_popupTime);
		
		m_time = 0;
		while(m_time < m_transitionTime) {
			yield return new WaitForEndOfFrame();
			m_ratio = Mathf.Lerp(m_transitionTime, 0, m_time/m_transitionTime) / m_transitionTime;
			m_time += Time.deltaTime;
		}
		
		Destroy(this.gameObject);
	}
	
	void OnGUI() {
		Rect rect = new Rect(0,0,0,0);
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperCenter;
		style.normal.textColor = Color.white;
		style.fontSize = m_fontSize;
		style.wordWrap = true;
		style.font = m_font;
		
		rect.width = style.CalcSize(new GUIContent(m_message)).x + m_fontSize;
		rect.height = style.CalcHeight(new GUIContent(m_message),rect.width);
		rect.x = Screen.width/2 - rect.width/2;
		rect.y = Screen.height - m_ratio * (style.CalcHeight(new GUIContent(m_message),rect.width) + m_fontSize/2);
		GUI.Label(rect, m_message, style);
	}
}
