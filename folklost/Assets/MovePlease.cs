using UnityEngine;
using System.Collections;
using RenPy;

public class MovePlease : MonoBehaviour {

	private bool move = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("win") && !move)
		{
			transform.Translate (0, -800f, 0, Space.World);
			move = true;
		}
	}
}
