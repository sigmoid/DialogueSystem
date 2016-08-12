﻿using System;

namespace DialogueSystem
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ConvoScriptParser parser = new ConvoScriptParser ();
			ConversationTree testTree = parser.Parse("DialogueSyntaxTest.txt");

			ConversationTraverser tv = new ConsoleTraverser (testTree);

			while (true)
				tv.Update ();
		}
	}
}
