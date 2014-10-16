using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerBoilerman : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("talked_boilerman"))
		{
			animation.CrossFade("Boilerman_IdleSimple", .5f);
		}
	}
}
