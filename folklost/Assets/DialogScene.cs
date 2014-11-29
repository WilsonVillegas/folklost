using UnityEngine;
using System.Collections;
using RenPy;

public class DialogScene : MonoBehaviour {

	public string m_scene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("dead"))
		{
			Application.LoadLevel(m_scene);
		}
	}
}
