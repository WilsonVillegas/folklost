using UnityEngine;
using System.Collections;

public class CameraCinematic : MonoBehaviour {
	
	public string folder;
	public int frameRate = 60;
	int num = 0;
	
	void Start() {
		Time.captureFramerate = frameRate;
		if(folder != null)
			System.IO.Directory.CreateDirectory("Screenshot/" + folder);
	}
	
	void Update() {
		//if(animation.isPlaying && folder != null) {
			Application.CaptureScreenshot("Screenshot/" + folder + "/" + num + ".png", 5);
			num++;
		//}
	}
}
