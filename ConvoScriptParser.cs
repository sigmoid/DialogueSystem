using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DialogueSystem
{
	public class ConvoScriptParser
	{
		Stack<ConversationNode> nodeStack;

		ConversationTree tree;

		public ConvoScriptParser ()
		{
			nodeStack = new Stack<ConversationNode> ();
		}

		public ConversationTree Parse(string filepath)
		{
			FileStream strm = new FileStream (filepath, FileMode.Open);
			StreamReader reader = new StreamReader(strm);

			while (!reader.EndOfStream) {
				ParseLine (reader.ReadLine());
			}

			return tree;
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
			ConversationNode tmpNode = new ConversationNode (dada [0]); 
			tmpNode.IndentLvl = indentLvl;

			if (tree == null) {
				tree = new ConversationTree (tmpNode);
				nodeStack.Push (tmpNode);
				return;
			}
			if (indentLvl == 0)
				return;

			while (nodeStack.Count > 0 && nodeStack.Peek ().IndentLvl >= indentLvl)
				nodeStack.Pop ();

			if(nodeStack.Count != 0)
				nodeStack.Peek().AddLink(new ConversationLink(tmpNode,dada[1]));

			nodeStack.Push (tmpNode);
		}
	}
}

