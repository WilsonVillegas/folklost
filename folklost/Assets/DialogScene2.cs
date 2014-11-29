using UnityEngine;
using System.Collections;
using RenPy;

public class DialogScene2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("lose"))
		{	
			Static.Variables.Clear();
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
