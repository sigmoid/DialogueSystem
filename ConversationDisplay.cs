using System;

namespace DialogueSystem
{
	public interface ConversationDisplay
	{
		void DisplayNode(ConversationNode cNode);
	}

	public class ConsoleDisplay : ConversationDisplay
	{
		public void DisplayNode(ConversationNode cNode)
		{
			Console.WriteLine ();
			Console.WriteLine (cNode.Line);
			Console.WriteLine ();
			int i = 0;
			foreach (ConversationLink link in cNode.Links) {
				Console.WriteLine ((i+1).ToString() + " - " + link.text);
				i++;
			}
			Console.WriteLine ("-------------------------------");
		}
	}
}

