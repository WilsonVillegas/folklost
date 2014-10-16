using UnityEngine;
using System.Collections.Generic;
using RenPy.Dialog;
using RenPy.Script;

namespace RenPy.Parser
{
	public class RenPyParser
	{
		public static RenPyDialogState Parse(ref RenPyScriptAsset script) {
			Tokenizer tokenizer = new Tokenizer();
			
			// Create the scanner
			LinkedList<string> tokens = new LinkedList<string>();
			for(int line = 0; line < script.Lines.Length; line++) {
				string[] arr = tokenizer.Tokenize(ref script.Lines[line]);
				for(int i = 0; i < arr.Length; i++) {
					tokens.AddLast(arr[i]);
				}
				tokens.AddLast("\n");
			}
			RenPyScanner scanner = new RenPyScanner(ref tokens);
			scanner.SkipWhitespace(true, true, true);
			
			// Parse the lines
			List<RenPyLine> lines = new List<RenPyLine>();
			while(scanner.HasNext()) {
				
				RenPyLine line = ParseLine(ref scanner);
				if(line != null) {
					lines.Add(line);
				}
				
				scanner.SkipWhitespace(true, true, true);
			}
			
			return new RenPyDialogState(script.Title, lines.ToArray());
		}
		
		private static RenPyLine ParseLine(ref RenPyScanner scanner) {
			switch(scanner.Peek()) {
				case "$":
					return new RenPyVariable(ref scanner);
				case "#":
					new RenPyComment(ref scanner);
					return null;
				case "image":
					new RenPyImage(ref scanner);
					return null;
				case "define":
					new RenPyCharacter(ref scanner);
					return null;
				case "label":
					return new RenPyLabel(ref scanner);
				case "if":
					return new RenPyIf(ref scanner);
				case "jump":
					return new RenPyJump(ref scanner);
				case "show":
					new RenPyShow(ref scanner);
					return null;
				case "scene":
					new RenPyScene(ref scanner);
					return null;
				case "play":
					return new RenPyPlay(ref scanner);
				case "stop":
					return new RenPyStop(ref scanner);
				case "menu":
					return new RenPyMenu(ref scanner);
				case "return":
					return new RenPyReturn(ref scanner);
				default:
					return new RenPySpeech(ref scanner);
			}
		}
	}
}
