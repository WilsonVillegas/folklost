namespace Twine.Util {
	
	/// <summary>
	/// Provides scanning functionality for a tokenizer.
	/// </summary>
	public class Scanner {
		
		private Tokenizer m_tokenizer;
		private int m_index = 0;
		
		/// <summary>
		/// Creates a new Scanner to scan the passed tokenizer.
		/// </summary>
		/// <param name="tokenizer">The tokenizer to scan</param>
		public Scanner(ref Tokenizer tokenizer) {
			m_tokenizer = tokenizer;
		}
		
		/// <summary>
		/// Returns the next token from the tokenizer.
		/// </summary>
		/// <returns>The next token in the tokenizer</returns>
		public string Next() {
			string token = m_tokenizer.Tokens[m_index];
			m_index++;
			return token;
		}
		
		/// <summary>
		/// Returns what the next token would be if Next() is called.
		/// </summary>
		/// <returns>The next token in the tokenizer</returns>
		public string Peek() {
			return m_tokenizer.Tokens[m_index];
		}
		
		/// <summary>
		/// Returns true if the Scanner will return more tokens when Next() is
		/// called.
		/// </summary>
		/// <returns>True if the next call to Next() will return a string</returns>
		public bool HasNext() {
			return m_index < m_tokenizer.Tokens.Length;
		}
	}
}
