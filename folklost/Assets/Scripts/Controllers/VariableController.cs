using UnityEngine;
using System.Collections;
using RenPy;

public class VariableController : MonoBehaviour {

	public string m_variableName;

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.A))
		   {
			Debug.Log("Setting " + m_variableName + "to true");
			Static.Variables[m_variableName] = "True";
		}
	}
}
