using System;

namespace DialogueSystem
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ConvoScriptParser parser = new ConvoScriptParser ();
			ConversationTree testTree = parser.Parse("DialogueSyntaxTest.txt");

			ConsoleTraverser tv = new ConsoleTraverser (testTree);

			while (tv.CanContinue)
				tv.Update ();


		}
	}
}
