using UnityEngine;
using System.Collections;
using RenPy;



public class VariableToggleController : MonoBehaviour {
	public GameObject Marcus;

	void Update () 
	{
		if(Marcus.transform.position.y > 5 && Marcus.transform.position.y  < 10)
		{
			Static.Variables["BAR"] = "False";
		}
		if(Marcus.transform.position.y  > 10)
		{
			Static.Variables["BAR"] = "True";
		}
	}

}
