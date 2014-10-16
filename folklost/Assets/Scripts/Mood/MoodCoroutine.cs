using UnityEngine;
using System.Collections;

public abstract class MoodCoroutine : MonoBehaviour {
	
	private MoodCoroutineHandler m_handler;
	
	/// <summary>
	/// Wrapper function for triggering a mood coroutine.
	/// </summary>
	/// <param name="hash">
	///     The hash of the mood zone that is calling this function
	/// </param>
	/// <param name="onEnter">
	///     Whether or not this is being called on enter
	/// </param>
	/// <param name="fadeTime">
	///     The amount of time to fade out this object's elements
	/// </param>
	public void Trigger(int hash, bool onEnter, float fadeTime) {
		// Create the coroutine handler if it doesn't exist yet
		m_handler = gameObject.GetComponent<MoodCoroutineHandler>();
		if(m_handler == null) {
			m_handler = gameObject.AddComponent<MoodCoroutineHandler>();
		}
		
		if(m_handler.CanTrigger(hash, onEnter)) {
			StopAllCoroutines();
			gameObject.SetActive(true); // TODO: Remove this bad hack
			StartCoroutine(Trigger(onEnter, fadeTime));
		}
	}
	
	/// <summary>
	/// Must be called when the trigger is finished.
	/// </summary>
	protected void Finish() {
		m_handler.Finish();
	}
	
	protected abstract IEnumerator Trigger(bool onEnter, float fadeTime);
}

class MoodCoroutineHandler : MonoBehaviour {
	
	private int m_currentMuteZone;
	private bool m_running = false;
	
	void Start() {
		m_running = false;
	}
	
	public bool CanTrigger(int hash, bool onEnter) {
		// Only trigger under the following conditions:
		//     * If we are entering
		//     * If we are exiting and we are not already exiting
		//     * If the request came from the same mood zone
		if(onEnter || (!onEnter && !m_running) || m_currentMuteZone == hash) {
			m_running = onEnter;
			m_currentMuteZone = hash;
			return true;
		}
		return false;
	}
	
	public void Finish() {
		m_running = false;
	}
}