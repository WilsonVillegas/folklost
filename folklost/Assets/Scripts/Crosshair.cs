using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	
	public enum Type {NONE, NORMAL, INSPECT, PICKUP, TALK};

	public Texture2D m_normalCrosshair;
	public Texture2D m_inspectCrosshair;
	public Texture2D m_pickupCrosshair;
	public Texture2D m_talkCrosshair;
	
	private Type m_currentType;
	private Texture2D m_currentCrosshair;
	
	void Start() {
		m_currentCrosshair = m_normalCrosshair;
	}
	
	void OnGUI() {
		if(m_currentCrosshair == null) {
			return;
		}
		
		float width = m_currentCrosshair.width;
		float height = m_currentCrosshair.height;
		float xMin = (Screen.width / 2) - (width / 2);
		float yMin = (Screen.height / 2) - (height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, width, height), m_currentCrosshair);
	}
	
	public void SetCrosshair(Type type) {
		m_currentType = type;
		switch(type) {
			case Type.NORMAL:
				m_currentCrosshair = m_normalCrosshair;
				break;
			case Type.INSPECT:
				m_currentCrosshair = m_inspectCrosshair;
				break;
			case Type.PICKUP:
				m_currentCrosshair = m_pickupCrosshair;
				break;
			case Type.TALK:
				m_currentCrosshair = m_talkCrosshair;
				break;
			default:
				m_currentCrosshair = null;
				break;
		}
	}
	
	public Type GetCurrentType() {
		return m_currentType;
	}
}
