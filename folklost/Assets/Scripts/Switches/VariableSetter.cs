using UnityEngine;
using System.Collections;

/// <summary>
/// Sets a set of variables to the specified values when a trigger area is entered
/// </summary>
public class VariableSetter : MonoBehaviour {
	
	public VariablePair[] m_variables;
	
	private bool m_on;
	
	void OnTriggerEnter(Collider col) {
		if(this.enabled && !m_on && col.tag == "Player") {
			m_on = true;
			foreach(VariablePair vp in m_variables) {
				RenPy.Static.Variables[vp.key] = vp.value;
			}
		}
	}
}
