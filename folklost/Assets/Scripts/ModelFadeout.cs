using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelFadeout : MonoBehaviour {
	
	public Material[] m_materials;
	public GameObject[] m_toDisable;
	public float m_time = 3f;
	
	private Dictionary<Material, float> m_originalValues;
	private Dictionary<Material, float> m_currentValues;
	
	void Awake() {
		m_originalValues = new Dictionary<Material,float>();
		m_currentValues = new Dictionary<Material,float>();
		foreach(Material m in m_materials) {
			m_originalValues.Add(m, m.color.a);
			m_currentValues.Add(m, m.color.a);
		}
	}
	
	void Start() {
		StartCoroutine(Fade());
	}
	
	void OnDestroy() {
		SetAlpha(m_originalValues);
	}
	
	private IEnumerator Fade() {
		float waited = 0;
		while(waited < m_time) {
			yield return new WaitForEndOfFrame();
			
			waited += Time.deltaTime;
			foreach(Material m in m_materials) {
				float alpha = Mathf.Lerp(m_originalValues[m], 0, waited/m_time);
				m_currentValues[m] = alpha;
			}
			SetAlpha(m_currentValues);
		}
		
		foreach(GameObject go in m_toDisable) {
			go.SetActive(false);
		}
		SetAlpha(m_originalValues);
	}
	
	private void SetAlpha(Dictionary<Material, float> alphas) {
		foreach(Material m in m_materials) {
			Color color = m.color;
			color.a = alphas[m];
			m.color = color;
		}
	}
}
