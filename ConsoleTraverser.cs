using System;

namespace DialogueSystem
{
	public interface ConversationTraverser{
		void Update();
	}

	public class ConsoleTraverser : ConversationTraverser
	{
		ConversationTree tree;
		ConversationNode cNode;
		ConversationDisplay cDisp;


		public ConsoleTraverser(ConversationTree tree){
			this.tree = tree;
			cNode = tree.Root;
			cDisp = new ConsoleDisplay ();

			cDisp.DisplayNode (cNode);
		}

		public void Update()
		{
			string inp = Console.ReadLine ();

			int iVal;
			if (int.TryParse (inp, out iVal)) {
				iVal -= 1;
				if (iVal < cNode.Links.Count) {
					cNode = cNode.Links [iVal].nextNode;
					cDisp.DisplayNode (cNode);
					return;
				}
			} else {
				Console.WriteLine (	"invalid input");
			}
		}
		
	}
}

