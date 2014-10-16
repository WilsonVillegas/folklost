using UnityEngine;
using System.Collections;

/// <summary>
/// Hides GameObjects by moving them to be under the water.
/// </summary>
public class OffscreenHider : MonoBehaviour {

	public GameObject m_gameObject;
	public float m_distance = 100;
	public bool m_hide = true;
	private bool m_oldHide;

	private float m_originalY;

	void Awake() {
		Vector3 pos = m_gameObject.transform.position;
		m_originalY = pos.y;
		m_oldHide = m_hide;
		if(m_hide) {
			Hide();
		}
	}

	void Update() {
		if(m_hide == m_oldHide) {
			return;
		}
		
		m_oldHide = m_hide;
		
		if(m_hide) {
			Hide();
		} else {
			Show();
		}
	}

	private void Hide() {
		Vector3 pos = m_gameObject.transform.position;
		pos.y = m_originalY - m_distance;
		m_gameObject.transform.position = pos;
	}

	private void Show() {
		Vector3 pos = m_gameObject.transform.position;
		pos.y = m_originalY;
		m_gameObject.transform.position = pos;
	}
}
