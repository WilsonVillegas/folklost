using UnityEngine;
using System.Collections;
using RenPy;

public class JukeboxBehaviour : Interactable {
	
	public AudioSource m_source;
	public AudioClip[] m_songs; 
	private int m_index;
	
	public void Start() {
		m_index = Random.Range(0, m_songs.Length-1);
		Static.Variables["song_playing"] = "False";
	}
	
	public override void Interact() {
		Static.Variables["song_playing"] = "True";
		Static.Variables["disc_spinning"] = "True";
		m_index++;
		if(m_index == m_songs.Length) {
			m_index = 0;
		}
		PlaySong(m_index);
	}
	
	public override void OnHoverEnter() {
		// Nothing to do
	}
	
	public override void OnHover() {
		// Nothing to do
	}
	
	public override void OnHoverExit() {
		// Nothing to do
	}
	
	public override string GetHoverText() {
		return "Play the next song on the jukebox";
	}

	private void PlaySong(int index) {
		m_source.clip = m_songs[index];
		m_source.Play();
	}
}
