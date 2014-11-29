using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{	

		transform.Translate (0, -.0002f, 0, Space.World);
		//if (Time.timeSinceLevelLoad > 15)
						//Application.LoadLevel(Application.loadedLevel);
	
	}
}
