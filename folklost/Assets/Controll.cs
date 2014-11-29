using UnityEngine;
using System.Collections;

public class Controll : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<PlayerController> ().LockControllers ();
		//GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
