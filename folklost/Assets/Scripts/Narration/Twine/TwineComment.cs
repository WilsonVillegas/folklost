using UnityEngine;
using System.Collections;
using Twine.Util;

namespace Twine {
	
	public class TwineComment {
		
		/// <summary>
		/// Creates a new TwineComment.
		/// </summary>
		/// <param name="text">The link source code</param>
		public TwineComment(ref Scanner scan) {
			scan.Next(); // Should be "/%"
			
			// Move the scanner until it gets to "%/"
			while(scan.Next() != "%/");
			
			if(scan.Peek() == "\n") {
				scan.Next(); // Should be "\n"
			}
		}
	}
}
