using UnityEngine;
using System.Collections;

public class AxeController : MonoBehaviour {

	public bool axe = false;
	public GameObject axemodel;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void AxeOnOff()
	{
		axemodel.SetActive(axe);
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
		{
			axemodel.SetActive(!axe);
			axe = !axe;
		}
	}
}
