using UnityEngine;
using System.Collections;
using RenPy;

public class RendererController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("lily_happy") && !Static.Variables.ContainsKey("finished_lily"))
			renderer.enabled = true;

	
	}
}
