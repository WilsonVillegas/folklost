using UnityEngine;

public class LightInteractable : Interactable {
	
	public AudioClip m_sound;
	public Light[] m_lights;
	public bool m_on = true;
	
	void Start() {
		UpdateLights();
	}

	public override void Interact() {
		AudioSource.PlayClipAtPoint(m_sound, this.transform.position);
		
		m_on = !m_on;
		UpdateLights();
	}
	
	public override void OnHoverEnter() {
		// Nothing to do
	}
	
	public override void OnHover() {
		// Nothing to do
	}
	
	public override void OnHoverExit() {
		// Nothing to do
	}
	
	public override string GetHoverText()
	{
		return m_on ? "Turn off" : "Turn on";
	}
	
	private void UpdateLights() {
		foreach(Light l in m_lights) {
			l.enabled = m_on;
		}
	}
}
