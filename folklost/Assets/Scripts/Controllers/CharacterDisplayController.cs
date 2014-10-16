using UnityEngine;
using System.Collections;
using RenPy;

public class CharacterDisplayController : MonoBehaviour {
	
	/// <summary>
	/// The Ren'Py variable to check. if it is equal to "True", then the
	/// character should be faded out.
	/// </summary>
	public string m_variable;
	public RenPyDisplayStyx m_dialog;
	public Renderer[] m_renderers;
	public AudioClip m_dissapearSound;
	public float m_waitTime;
	public Bloom m_bloom;
	
	private bool triggered = false;
	private int time;
	
	void Start () 
	{
		if(string.IsNullOrEmpty(m_variable)) {
			Debug.LogWarning("Variable needs to be set to a non-null and non-empty value!");
		}
		time = 0;
	}
	
	void Update () 
	{
		if(string.IsNullOrEmpty(m_variable)) {
			return;
		}
		
		if(Static.Variables.ContainsKey(m_variable) && Static.Variables[m_variable] == "True") {
			if(!triggered) {
				triggered = true;
				StartCoroutine(Bloom());
			}
			if(time < 200)
			{
				foreach(Renderer renderer in m_renderers) {
					if(renderer.enabled)
					{
						renderer.enabled = false;
					}
					else if(!renderer.enabled && time % 7 == 0)
					{
						renderer.enabled = true;
					}
				}
				
				time++;
			}
			else {
				foreach(Renderer renderer in m_renderers) {
					renderer.enabled = false;
				}
			}
			m_dialog.gameObject.collider.enabled = false;
		} else {
			foreach(Renderer renderer in m_renderers) {
				renderer.enabled = true;
			}
			m_dialog.gameObject.collider.enabled = true;
		}
	}

	private IEnumerator Bloom() {
		AudioSource.PlayClipAtPoint(m_dissapearSound, m_dialog.gameObject.transform.position);
		float m_time = 0;
		while(m_time < m_waitTime) {
			yield return new WaitForEndOfFrame();
			m_bloom.bloomIntensity = Mathf.Lerp(0, 3, 1-(Mathf.Abs(m_waitTime-m_time*2)/m_waitTime));
			m_time += Time.deltaTime;
		}
	}
}