using UnityEngine;
using System.Collections;

public class CameraShakeController : MonoBehaviour {
	
	private const float m_defaultIntensity = .04f;
	private const float m_defaultTime = 2f;
	
	public void Shake(float time = m_defaultTime, float intensity = m_defaultIntensity) {
		StartCoroutine(ShakeRoutine(time, intensity));
	}

	IEnumerator ShakeRoutine(float time, float intensity) {
		while(time > 0) {
			yield return new WaitForEndOfFrame();
			time -= Time.deltaTime;
			float angle = Random.Range(0, 2 * Mathf.PI);
			Vector3 shake = new Vector3();
			shake.x = Mathf.Cos(angle) * Random.Range(0, intensity);
			shake.y = Mathf.Sin(angle) * Random.Range(0, intensity);
			transform.localPosition = shake;
		}
		
		transform.localPosition = Vector3.zero;
	}
}
