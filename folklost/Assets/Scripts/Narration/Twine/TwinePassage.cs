using UnityEngine;
using System;
using System.Collections.Generic;
using Twine.Util;

namespace Twine {
	
	/// <summary>
	/// Represents a passage in Twine.
	/// </summary>
	public class TwinePassage
	{
		#region Properties
		
		private string m_title;
		private List<string> m_tags;
		private TwineLine[] m_lines;
		private TwineLink[] m_links;
		
		/// <summary>
		/// The title of this passage
		/// </summary>
		public string Title {
			get { return m_title; }
		}
		
		/// <summary>
		/// The tags for this passage
		/// </summary>
		public List<string> Tags {
			get { return m_tags; }
		}
		
		/// <summary>
		/// The lines in this passage
		/// </summary>
		public TwineLine[] Lines {
			get { return m_lines; }
		}
		
		/// <summary>
		/// The links in this passage
		/// </summary>
		public TwineLink[] Links {
			get { return m_links; }
		}
		
		#endregion
		
		/// <summary>
		/// Parses a Twine passage from the string given to it.
		/// </summary>
		/// <param name="passage">
		/// The Twine passage as it appears in Twee source code without the leading ":: " string
		/// </param>
		public TwinePassage(ref Scanner scan) {
			m_tags = new List<string>();
			ParseMeta(ref scan);
			
			// Don't parse the Start passage
			if(m_title == "Start") {
				return;
			}
			// Don't parse script passages
			if(m_tags.Contains("script")) {
				return;
			}
			
			List<TwineLine> lines = new List<TwineLine>();
			List<TwineLink> links = new List<TwineLink>();
			
			List<TwineJump> jumps = new List<TwineJump>();
			TwineConditional lastConditional = null;
			TwineConditional lastIfConditional = null;
			while(scan.HasNext()) {
				string mode = scan.Peek();
				
				// Got to a new paragraph
				if(mode == "::") {
					break;
				}
				
				// Parse TwineComment
				else if(mode == "/%") {
					new TwineComment(ref scan); // Don't store the comment
				}
				
				// Parse TwineLink
				else if(mode == "[[") {
					TwineLink link = new TwineLink(ref scan);
					link.Conditional = lastIfConditional;
					links.Add(link);
				}
				
				// Parse TwineVariable
				else if(mode == "<<") {
					scan.Next(); // Should be <<
					mode = scan.Peek();
					if(mode == "set") {
						lines.Add(new TwineVariable(ref scan));
					}
					else if(mode == "if" || mode == "else") {
						if(mode == "else") {
							// Add a jump
							TwineJump jump = new TwineJump();
							jumps.Add(jump);
							lines.Add(jump);
						}
						
						// Parse the conditional
						TwineConditional conditional = new TwineConditional(ref scan);
						lines.Add(conditional);
						
						// Update the previous conditional
						if(lastConditional != null && mode != "if") {
							lastConditional.TargetLine = lines.Count-1;
						}
						lastConditional = conditional;
						
						// Update the previous if conditional
						if(mode == "if") {
							lastIfConditional = conditional;
						}
					}
					else if(mode == "endif") {
						// Parse the conditional
						TwineConditional conditional = new TwineConditional(ref scan);
						lines.Add(conditional);
						
						// Update the previous conditional
						if(lastConditional != null) {
							lastConditional.TargetLine = lines.Count-1;
						}
						lastConditional = conditional;
						lastIfConditional = null;
						
						// Update jumps
						foreach(TwineJump jump in jumps) {
							jump.TargetLine = lines.Count-1;
						}
						jumps.Clear();
					}
					else if(mode == "jump") {
						lines.Add(new TwineMacroJump(ref scan));
					}
					else {
						Debug.LogWarning("Macro \"" + mode + "\" not supported.");
					}
				}
				
				// Parse TwineLine
				else {
					lines.Add(new TwineSpeech(ref scan));
				}
			}
			
			m_lines = lines.ToArray();
			m_links = links.ToArray();
		}
		
		private void ParseMeta(ref Scanner scan) {
			// Parse the title
			while(scan.HasNext()) {
				string token = scan.Next();
				if(token == "\n") {
					return;
				} else if(token == "[") {
					break;
				}
				
				if(m_title == null) {
					m_title = token;
				} else {
					m_title += " " + token;
				}
			}
			
			// Parse the tags
			List<string> tags = new List<string>();
			while(scan.HasNext()) {
				string token = scan.Next();
				if(token == "\n") {
					return;
				} else if(token == "]") {
					continue;
				}
				
				tags.Add(token);
			}
			m_tags = tags;
		}
	}
}
