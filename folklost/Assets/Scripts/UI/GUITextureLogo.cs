using UnityEngine;
using System.Collections;

public class GUITextureLogo : MonoBehaviour {
	
	public GUITexture m_texture;
	
	void Update () {
		float screenRatio = (float)Screen.width / (float)Screen.height;
		Rect rect = m_texture.pixelInset;
		rect.width = m_texture.texture.width * screenRatio / 3;
		rect.height = m_texture.texture.height * screenRatio / 3;
		rect.x = -rect.width/2;
		rect.y = -rect.height/2;
		m_texture.pixelInset = rect;
	}
}
