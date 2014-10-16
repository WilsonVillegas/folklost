using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerWomanEnd : MonoBehaviour {

	public bool talked_alice_end = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("talked_alice_end"))
		{
			animation["Wwoman_talking"].speed = .1f;
			animation.CrossFade("Wwoman_talking", 1f);
			talked_alice_end = true;
		}
	}
}
