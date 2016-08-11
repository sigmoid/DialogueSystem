using System;
using System.Collections.Generic;

namespace DialogueSystem
{
	public class ConversationTree
	{
		public ConversationNode Root { get ; private set;}

		public ConversationTree (ConversationNode RootNode)
		{
			Root = RootNode;
		}

	}

	public class ConversationNode
	{
		public static Dictionary<string, ConversationNode> NodeTable;

		public string ID { get; private set;}

		public List<ConversationLink> Links;

		public string Line;

		public ConversationNode( string line, string ID = "noname")
		{
			if (NodeTable == null)
				NodeTable = new Dictionary<string,ConversationNode> ();

			Links = new List<ConversationLink>();
			Line = line;
			this.ID = ID;

			if (ID != "noname") {
				NodeTable.Add (ID, this);
			}
		}

		public void AddLink(ConversationLink lnk){
			Links.Add (lnk);
		}

		public static void AddLink(string parentID, ConversationLink lnk)
		{
			NodeTable [parentID].AddLink (lnk);
		}
	}

	public class ConversationLink
	{
		public ConversationLink(ConversationNode nextnode, string text)
		{
			nextNode = nextnode;
			this.text = text;
		}

		public ConversationNode nextNode;

		public string text;
	}

}

