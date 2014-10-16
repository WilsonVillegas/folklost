using UnityEngine;
using System.Collections;

public class Escape : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			pauseGame();
		}
	}

	public void pauseGame(){
		if (Time.timeScale == 1.0F){
			Time.timeScale = 0.0F;
			AudioListener.pause = true;
			//Component script3 = GetComponent("PauseMenu");// GameObject.Find("PauseMenu"); 
			//script3.SetActive(true);
			(gameObject.GetComponent("PauseMenu") as MonoBehaviour).enabled = true;
		}else{
			Time.timeScale = 1.0F;
			AudioListener.pause = false;
			//GameObject script3 = GameObject.Find("PauseMenu"); 
			//script3.SetActive(false);
			(gameObject.GetComponent("PauseMenu") as MonoBehaviour).enabled = false;
		}
	}
}
