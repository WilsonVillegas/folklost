using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerJukebox : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Lily cowers
		if(Static.Variables.ContainsKey("disc_spinning"))
		{
			animation.CrossFade("DiscSpin", 2f);
		}
	}
}
