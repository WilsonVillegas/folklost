using UnityEngine;
using System.Collections;

public class AxeController : MonoBehaviour {

	public bool axe = false;
	public bool gotAxe;
	public GameObject axemodel;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseOver()
	{
		axemodel.SetActive(true);
		axe = false;
		gotAxe = true;
		GetComponent<ItemInteractable> ().Interact ();
	}
}
