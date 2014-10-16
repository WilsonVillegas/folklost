using UnityEngine;
using System.Collections;
using System.IO;
using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyPlay : RenPyLine
	{
		private string m_channel;
		private string m_file;
		private bool m_loop;
		private float m_fadein;
		private float m_fadeout;
		
		public RenPyPlay(ref RenPyScanner tokens) : base(RenPyLineType.PLAY) {
			tokens.Seek("play");
			tokens.Next();
			
			// Get channel and setup default looping behaviour
			tokens.Seek(new string [] {"music", "sound"});
			m_channel = tokens.Next();
			m_loop = (m_channel == "music" ? true : false);
			
			// TODO: support this feature
			if(tokens.PeekIgnoreWhitespace(true,true,true) == "[") {
				Debug.LogError("Multiple play statement not supported yet");
			}
			
			// Get the filename
			tokens.Seek("\"");
			tokens.Next();
			m_file = tokens.Seek("\"");
			tokens.Next();
			
			// Parse any recognized clauses
			bool nothing = false;
			while(!nothing) {
				string token = tokens.PeekIgnoreWhitespace(true,true,true);
				switch(token) {
					case "loop":
						tokens.Seek("loop");
						tokens.Next();
						m_loop = true;
						break;
					case "noloop":
						tokens.Seek("noloop");
						tokens.Next();
						m_loop = false;
						break;
					case "fadein":
						tokens.Seek("fadein");
						tokens.Next();
						tokens.SkipWhitespace();
						m_fadein = float.Parse(tokens.Next());
						m_fadein = m_fadein < 0 ? 0 : m_fadein;
						break;
					case "fadeout":
						tokens.Seek("fadeout");
						tokens.Next();
						tokens.SkipWhitespace();
						m_fadeout = float.Parse(tokens.Next());
						m_fadeout = m_fadeout < 0 ? 0 : m_fadeout;
						break;
					default:
						nothing = true;
						break;
				}
			}
		}
		
		public override void Execute(RenPyDisplay display) {
			string str = "play " + m_channel + " ";
			str += "file:\"" + m_file + "\" ";
			str += (m_loop ? "loop " : "noloop ");
			str += (m_fadein > 0 ? "fadein:" + m_fadein + " " : "");
			str += (m_fadeout > 0 ? "fadeout:" + m_fadeout + " " : "");
			Static.LogRenPy(str);
			
			// Get the filename
			string file = display.State.ResourcePath + m_file;
			string ext = Path.GetExtension(file);
			file = file.Replace(ext,""); // TODO: Make this safer
			
			// Get the audio file
			AudioClip clip = Resources.Load<AudioClip>(file);
			if(clip == null) {
				Debug.LogError("Could not file AudioClip \""+file+"\"");
			}
			
			// Play the audio file
			display.BeginCoroutine(m_channel, PlayAudio(display, clip));
		}
		
		public override string ToString() {
			return base.ToString() + ": play " + m_channel + " \"" + m_file + "\" " + (m_loop ? "loop" : "noloop");
		}
		
		private IEnumerator PlayAudio(RenPyDisplay display, AudioClip clip) {
			AudioSource channel =  GetChannel(display);
			
			// Fade out
			float start = channel.volume;
			float time = 0;
			if(channel.clip != clip) {
				while(time < m_fadeout) {
					yield return new WaitForFixedUpdate();
					time += Time.fixedDeltaTime;
					channel.volume = Mathf.Lerp(start, 0, time/m_fadeout);
				}
				channel.volume = 0;
			}
			
			// Change the audio
			channel.clip = clip;
			channel.loop = m_loop;
			channel.Play();
			
			// Fade in
			start = channel.volume;
			time = 0;
			while(time < m_fadein) {
				yield return new WaitForFixedUpdate();
				time += Time.fixedDeltaTime;
				channel.volume = Mathf.Lerp(start, 1, time/m_fadein);
			}
			channel.volume = 1;
		}

		private AudioSource GetChannel(RenPyDisplay display) {
			switch(m_channel) {
				case "music":
					return display.Music;
				case "sound":
					return display.Sound;
			}
			return null;
		}
	}
}
