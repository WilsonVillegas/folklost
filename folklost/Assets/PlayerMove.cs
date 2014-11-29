using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	private int index = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Eat"))
		{
		    transform.Translate (0, -.008f, 0, Space.World);
			index++;
		}
	
		if(index >= 6)
		{
			//GetComponent<PlayerController> ().ReleaseControllers ();
			Application.LoadLevel("Cave Final");
		}
	}
}
