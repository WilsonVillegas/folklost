using UnityEngine;
using System.Collections;

public class FogCoroutine : MoodCoroutine {
	
	private Color m_color;
	public Color Color {
		get { return m_color; }
		set { m_color = value; }
	}
	
	private float m_density;
	public float Density {
		get { return m_density; }
		set { m_density = value; }
	}
	
	protected override IEnumerator Trigger(bool onEnter, float fadeout) {
		if(onEnter) {
			Color startColor = RenderSettings.fogColor;
			float startDensity = RenderSettings.fogDensity;
			
			float time = 0;
			while(time < fadeout) {
				yield return new WaitForFixedUpdate();
				time += Time.fixedDeltaTime;
				
				RenderSettings.fogDensity = Mathf.Clamp(Mathf.Lerp(startDensity, m_density, time/fadeout), 0, 1);
				RenderSettings.fogColor = Color.Lerp(startColor, m_color, time/fadeout);
			}
			
			Finish();
		}
	}
}
