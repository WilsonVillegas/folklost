using UnityEngine;
using System.Collections;

public class ToastMachine : MonoBehaviour {
	
	public GameObject lastToast;

	private static ToastMachine m_instance;
	public static ToastMachine Instance {
		get { return m_instance; }
	}
	
	public Font m_font;
	
	void Start() {
		if(m_instance == null) {
			m_instance = this;
		} else {
			Debug.LogError("Cannot have more than one instance of ToastMachine");
		}
	}
	
	void OnDestroy() {
		if(m_instance == this) {
			m_instance = null;
		}
	}
	
	public void Toast(string message) {
		if(lastToast != null) {
			Destroy(lastToast);
		}
		
		lastToast = new GameObject("Toast");
		lastToast.transform.parent = this.transform;
		Toast toast = lastToast.AddComponent<Toast>();
		toast.Message = message;
		toast.PopupTime = 2f;
		toast.ToastFont = m_font;
		toast.FontSize = UIStatic.FontSize;
	}
}
