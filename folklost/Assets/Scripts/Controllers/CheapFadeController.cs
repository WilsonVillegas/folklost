using UnityEngine;
using System.Collections;
using RenPy;

public class CheapFadeController : MonoBehaviour {

	public int time;
	public bool once;
	// Use this for initialization
	void Start ()
	{
		time = 0;
		once = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("finished_lily"))
		   {
			if(time < 200)
			{
				if(renderer.enabled)
				{
					renderer.enabled = false;
				}
				else if(!renderer.enabled && time % 7 == 0)
				{
					renderer.enabled = true;
				}
			time++;
	
			}
		}
	}
}
