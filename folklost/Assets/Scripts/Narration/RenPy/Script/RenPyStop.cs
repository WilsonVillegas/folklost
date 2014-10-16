using UnityEngine;
using System.Collections;
using System.IO;
using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyStop : RenPyLine
	{
		private string m_channel;
		
		private float m_fadeout;
		
		public RenPyStop(ref RenPyScanner tokens) : base(RenPyLineType.STOP) {
			tokens.Seek("stop");
			tokens.Next();
			
			tokens.Seek(new string [] {"music", "sound"});
			m_channel = tokens.Next();
			
			// Parse any recognized clauses
			bool nothing = false;
			while(!nothing) {
				string token = tokens.PeekIgnoreWhitespace(true,true,true);
				switch(token) {
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
			Static.LogRenPy("stop " + m_channel + (m_fadeout > 0 ? " fadeout:" + m_fadeout : ""));
			
			// Stop the audio
			display.BeginCoroutine(m_channel, StopAudio(display));
		}
		
		private IEnumerator StopAudio(RenPyDisplay display) {
			AudioSource channel =  GetChannel(display);
			
			// Fade out
			float time = 0;
			while(time < m_fadeout) {
				yield return new WaitForFixedUpdate();
				time += Time.fixedDeltaTime;
				channel.volume = Mathf.Lerp(1, 0, time/m_fadeout);
			}
			
			// Change the audio
			channel.volume = 0;
			channel.clip = null;
			channel.Stop();
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
		
		public override string ToString() {
			return base.ToString() + ": stop " + m_channel;
		}
	}
}
