using System;

namespace DialogueSystem
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ConversationTree testTree = new ConversationTree(new ConversationNode("Hello, world!", "root"));
			testTree.Root.AddLink(new ConversationLink(new ConversationNode("That's not nice..."), "I don't like you..."));
			testTree.Root.AddLink(new ConversationLink(new ConversationNode("I'm ... Okay? I really like tomatoes", "good"), "How are you"));
			testTree.Root.AddLink(new ConversationLink(new ConversationNode("It's Frank."), "What's your name?"));
			testTree.Root.Links[2].nextNode.AddLink(new ConversationLink("root", "Let's start over"));
			ConversationNode.AddLink ("good", new ConversationLink (new ConversationNode ("What  ?", "wut"), "Yeah, it's a pretty good nose I guess"));
			ConversationNode.AddLink ("wut", new ConversationLink ("good", "Wait, what did you say? I thought you said \"my nose\""));
				

			ConversationTraverser tv = new ConsoleTraverser (testTree);

			while (true)
				tv.Update ();
		}
	}
}
