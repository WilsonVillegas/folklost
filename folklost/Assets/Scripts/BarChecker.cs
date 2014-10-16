using UnityEngine;
using System.Collections;
using RenPy;

public class BarChecker : MonoBehaviour {

	void Start () {
		Static.Variables["behind_Bar"] = "False";
	}

	void OnTriggerStay(){
		Static.Variables["behind_Bar"] = "True";
	}

	void OnTriggerExit(){
		Static.Variables["behind_Bar"] = "False";
	}
}
