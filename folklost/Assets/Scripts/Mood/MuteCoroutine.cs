using UnityEngine;
using System.Collections;

public class MuteCoroutine : MoodCoroutine {
	
	private bool m_mute;
	public bool Mute {
		get { return m_mute; }
		set { m_mute = value; }
	}
	
	private AudioSource m_source;
	
	void Awake() {
		m_source = this.GetComponent<AudioSource>();
	}
	
	protected override IEnumerator Trigger(bool onEnter, float fadeout) {
		float start = m_source.volume;
		float time = 0;
		bool mute = (m_mute && onEnter) || (!m_mute && !onEnter);
		
		while(time < fadeout) {
			yield return new WaitForFixedUpdate();
			time += Time.fixedDeltaTime;
			
			if(mute) {
				m_source.volume = Mathf.Clamp(Mathf.Lerp(start, 0, time/fadeout), 0, 1);
			} else {
				m_source.volume = Mathf.Clamp(Mathf.Lerp(start, 1, time/fadeout), 0, 1);
			}
		}
		
		Finish();
	}
}
