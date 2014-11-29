using UnityEngine;
using System.Collections;

public class PickMeUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnMouseOver () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			ToastMachine.Instance.Toast("Picked up Silver Wood piece");
			gameObject.SetActive(false);
		}
	}
}
