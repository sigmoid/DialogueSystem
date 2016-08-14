using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DialogueSystem
{
	public class ConvoScriptParser
	{
		Stack<ConversationNode> nodeStack;

		Dictionary<string, ConversationNode> NodeTable;

		ConversationTree tree;

		public ConvoScriptParser ()
		{
			NodeTable = new Dictionary<string, ConversationNode> ();
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

		private void CleanupWhiteSpace(ref string line, int indentLvl)
		{
			//Clean up the text a bit
			line = line.Remove (0, indentLvl);

			//Removes any whitespace outside of quotes, then removes the quotes.
			string regplace = "\\s+(?=((\\\\[\\\\\"]|[^\\\\\"])*\"(\\\\[\\\\\"]|[^\\\\\"])*\")*(\\\\[\\\\\"]|[^\\\\\"])*$)";
			line = Regex.Replace (Regex.Replace (line, regplace, ""), "\"", "");
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

			CleanupWhiteSpace (ref line,indentLvl);

			//Split the line into parts
			string[] dada = line.Split ('|');
			ConversationNode tmpNode = new ConversationNode (dada [0]); 
			tmpNode.IndentLvl = indentLvl;

			string name = "null";
			if (dada.Length > 2)
				name = dada [2];

			//IF the tree is uninstantiated, instantiate it with a root node and return
			if (tree == null) {
				tree = new ConversationTree (tmpNode);
				nodeStack.Push (tmpNode);
				if(name != "null")
					NodeTable.Add (name,tmpNode);
				return;
			}
			
			if (dada [0].Contains ("FROM")) {
				nodeStack.Clear ();
				Match nodeName = Regex.Match (dada [0], "FROM(.*)");
				ConversationNode nde = NodeTable [nodeName.Groups [1].Value];  
				nde.IndentLvl = 0;
				nodeStack.Push (nde);

				return;
			}

			while (nodeStack.Count > 0 && nodeStack.Peek ().IndentLvl >= indentLvl)
				nodeStack.Pop ();

			if (nodeStack.Count != 0) {
				if (dada [0].Contains ("GOTO")) {

					Match nodeName = Regex.Match (dada [0], "GOTO(.*)");
					AppendNode (new ConversationLink (NodeTable [nodeName.Groups [1].Value], dada [1]), name);
					return;
				}
				AppendNode( new ConversationLink (tmpNode, dada [1]),name);
			} 


			nodeStack.Push (tmpNode);
		}
	

		void AppendNode(ConversationLink lnk, string name)
		{
			tree.AddLink (nodeStack.Peek (),lnk);
			if (name != "null" && !NodeTable.ContainsValue(lnk.nextNode))
				NodeTable.Add (name, lnk.nextNode);
		}

	}
}

