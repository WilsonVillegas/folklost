using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public Transform Destination;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other) 
	{
		Vector3 dst = new Vector3(Destination.position.x, Destination.position.y, Destination.position.z);
		if (other.gameObject.tag == "Player")

						other.gameObject.transform.position = dst;
	}
}
