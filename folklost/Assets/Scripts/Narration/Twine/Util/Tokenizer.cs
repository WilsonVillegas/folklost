using System.Collections.Generic;
using UnityEngine;

namespace Twine.Util {
	
	/// <summary>
	/// A class that can produce a sequence of tokens from a string that
	/// represents Twine source code.
	/// </summary>
	public class Tokenizer {
		
		#region Properties
		
		/// <summary>
		/// A list of token definitions that this Tokenizer will attempt to
		/// match.
		/// </summary>
		private List<TokenDefinition> m_tokenDefinitions;
		
		/// <summary>
		/// An array of tokens that this Tokenizer has generated as a result of
		/// a call to Tokenize().
		/// </summary>
		private string[] m_tokens;
		
		/// <summary>
		/// An array of tokens that this Tokenizer has generated as a result of
		/// a call to Tokenize(). Will return null if Tokenize() has not yet
		/// been called.
		/// </summary>
		public string[] Tokens {
			get { return m_tokens; }
		}
		
		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Creates a new tokenizer that tokenizes the passed string.
		/// </summary>
		/// <param name="str">
		/// A string containing the Twine source code to tokenize
		/// </param>
		public Tokenizer() {
			m_tokenDefinitions = new List<TokenDefinition>();
			
			// Setup parsing for Twine tokens
			string[] tokens;
			tokens = ":: [[ ]] [ ] {{ }} << >> /% %/ | : ;".Split(' ');
			foreach(string str in tokens)
				m_tokenDefinitions.Add(new TokenDefinition(str));
			
			// Setup parsing for formatting tokens
			tokens = "// '' __ ==".Split(' ');
			foreach(string str in tokens)
				m_tokenDefinitions.Add(new TokenDefinition(str));
			
			// Setup parsing for variable setting
			m_tokenDefinitions.Add(new TokenDefinition("="));
		}
		
		#endregion
		
		#region Member Methods
		
		/// <summary>
		/// Tokenizes a string by identifying TokenDefinitions and seperating by
		/// whitespace. Treats newlines as a token.
		/// </summary>
		/// <param name="str">The string to tokenize</param>
		public void Tokenize(string str) {
			List<string> tokens = new List<string>();
			char[] chars = str.ToCharArray();
			int index = -1;
			int length = chars.Length;
			
			string currentToken = "";
			bool newlineAdded = false;
			
			while(index < length-1) {
				// Get the next character
				index++;
				char ch = chars[index];
				
				// Check for whitespace
				if(ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r') {
					
					// Add the current token
					if(AddToken(ref currentToken, ref tokens)) {
						newlineAdded = false;
					}
					
					// Treat '\n' as a token, unless the last token was '\n'
					if((ch == '\n' || ch == '\r') && !newlineAdded) {
						tokens.Add("\n");
						newlineAdded = true;
					}
					
					// Start the loop over
					continue;
				}
				
				// Check for token definitions
				bool broke = false;
				foreach(TokenDefinition def in m_tokenDefinitions) {
					string token;
					if(def.HasSequence(ref index, ref chars, out token)) {
						
						// Add the current token
						if(AddToken(ref currentToken, ref tokens)) {
							newlineAdded = false;
						}
						
						// Add the found token definition
						tokens.Add(token);
						
						// Start the loop over
						broke = true;
						break;
					}
				}
				if(broke) {
					continue;
				}
				
				// If no special cases were found, build the current token
				currentToken += ch;
			}
			
			m_tokens = tokens.ToArray();
		}
		
		/// <summary>
		/// Adds a token to a list of tokens and resets it back to an empty
		/// string if it is not an empty string.
		/// </summary>
		/// <param name="token">The token to add</param>
		/// <param name="tokens">The token list to add the token to</param>
		/// <returns>True if the token was added</returns>
		private bool AddToken(ref string token, ref List<string> tokens) {
			if(token != "") {
				tokens.Add(token);
				token = "";
				return true;
			}
			return false;
		}
		
		#endregion
	}
}