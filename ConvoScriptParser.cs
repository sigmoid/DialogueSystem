using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DialogueSystem
{
	public class ConvoScriptParser
	{
		Stack<ConversationNode> nodeStack;
		int prevIndent = 0;

		ConversationTree tree;

		public ConvoScriptParser ()
		{
			nodeStack = new Stack<ConversationNode> ();
		}

		public void Parse(string filepath)
		{
			FileStream strm = new FileStream (filepath, FileMode.Open);
			StreamReader reader = new StreamReader(strm);

			while (!reader.EndOfStream) {
				ParseLine (reader.ReadLine());
			}
		}

		private void ParseLine(string line)
		{
			char[] linearray = line.ToCharArray ();

			//Ignore empty lines and comment lines
			if (linearray.Length == 0)
				return;
			if (linearray[0] == '#')
				return;

			int indentLvl = 0;

			if (linearray [0] == '~') {
				indentLvl = 0;
				while (linearray [indentLvl] == '~') {
					indentLvl++;
				}
			}

			line = line.Remove (0, indentLvl);

			string[] dada = line.Split (':');
			ConversationNode tmpNode = new ConversationNode (dada [0], dada [1]); 

			if (tree == null) {
				tree = new ConversationTree (tmpNode);
				prevIndent = indentLvl;
				return;
			}
			if (indentLvl == 0)
				return;

			if (indentLvl <= prevIndent) {
				for(int i = 0 ;i < prevIndent-indentLvl;i++)
					nodeStack.Pop ();
			}

			prevIndent = indentLvl;
			Console.WriteLine (line);
		}
	}
}

