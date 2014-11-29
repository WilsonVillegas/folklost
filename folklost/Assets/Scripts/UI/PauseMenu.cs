using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	public Texture2D logo;
	public Texture2D resume;
	public Texture2D restart;
	public Texture2D quit;
	public Font m_font;
	//public Blur blur;

	private bool m_open;
	public bool Open {
		get { return m_open; }
	}
	
	void Update () {
		//blur.enabled = m_open;
		if(Input.GetKeyDown(KeyCode.Escape) && m_open == false) {
			PauseGame();
		}
		
		if(m_open) {
			Screen.lockCursor = false;
		}
	}

	void OnGUI() {
		if(!m_open) {
			return;
		}
		
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.fontSize = 4*UIStatic.FontSize/5;
		style.font = m_font;
		
		//layout start
		float w = 300;
		float h = 375;
		float w2 = w/2;
		float h2 = h/2;
		float sw2 = Screen.width/2;
		float sh2 = Screen.height/2;
		GUI.BeginGroup(new Rect(sw2 - w2, sh2 - h2, w, h));
		
		//the menu background box
		GUI.Box(new Rect(0, 0, w, h), "");
		
		//logo picture
		GUI.Label(new Rect(w2-50, 10, 300, 68), logo);
		
		//game resume button
		if(GUI.Button(new Rect(110, 100, 180, 40), resume, GUIStyle.none)) {
			ResumeGame();
		}
		
		//main menu return button (level 0)
		if(GUI.Button(new Rect(110, 150, 180, 40), restart, GUIStyle.none)) {
			ResumeGame();
			RenPy.Static.Variables.Clear();
			Application.LoadLevel ("Title");
		}
		
		//quit button
		if(GUI.Button(new Rect(110, 200, 180, 40), quit, GUIStyle.none)) {
			Application.Quit();
		}
		
		// Inverted
		PlayerController pc = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
		pc.Inverted = GUI.Toggle(new Rect(10, 270, 120, 100), pc.Inverted, "Inverted Mouse");
		GUI.Label(new Rect(10,300,280,30), "Mouse Sensitivity");
		pc.Sensitivity = GUI.HorizontalSlider(new Rect(10, 330, w-20, 30), pc.Sensitivity, 20f, 500f);
		
		GUI.EndGroup(); 
	}
	
	private void PauseGame() {
		Time.timeScale = 0.0f;
		AudioListener.pause = true;
		m_open = true;
		ToastMachine tm = GameObject.Find("ToastMachine").GetComponent<ToastMachine>();
		GameObject.Destroy(tm.lastToast);
		tm.lastToast = null;
	}
	
	private void ResumeGame() {
		Time.timeScale = 1.0f;
		AudioListener.pause = false;
		m_open = false;
	}
}
