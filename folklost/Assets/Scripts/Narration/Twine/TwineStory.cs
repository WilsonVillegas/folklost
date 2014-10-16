using UnityEngine;
using System;
using System.Collections.Generic;
using Twine.Util;

namespace Twine {
	
	/// <summary>
	/// Represents a story in Twine.
	/// </summary>
	public class TwineStory {
		
		/// <summary>
		/// A dictionary of passage titles to passages.
		/// </summary>
		private Dictionary<string, TwinePassage> m_passages;
		
		/// <summary>
		/// Parses a Twine story from the source given to it.
		/// </summary>
		/// <param name="twee">The source code of the story</param>
		public TwineStory(TextAsset twee) {
			m_passages = new Dictionary<string,TwinePassage>();
			
			// Tokenize the Twine source code
			Tokenizer tokenizer = new Tokenizer();
			tokenizer.Tokenize(twee.text);
			
			// Parse the passages
			Scanner scan = new Scanner(ref tokenizer);
			while(scan.HasNext()) {
				string token = scan.Next();
				if(token == "::") {
					TwinePassage passage = new TwinePassage(ref scan);
					m_passages.Add(passage.Title, passage);
				}
			}
		}
		
		/// <summary>
		/// Returns the specified passage.
		/// </summary>
		/// <param name="passageTitle">The title of the passage to get</param>
		/// <returns></returns>
		public TwinePassage GetPassage(string passageTitle) {
			return m_passages[passageTitle];
		}
	}
}
