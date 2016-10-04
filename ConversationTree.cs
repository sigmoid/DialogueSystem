using System;
using System.Collections.Generic;

namespace DialogueSystem
{
	public class ConversationTree
	{
        /// <summary>
        /// fakechange
        /// </summary>
		public ConversationNode Root { get ; private set;}

		private List<ConversationNode> Nodes;
		private List<ConversationLink> Links;

		public ConversationTree (ConversationNode RootNode)
		{
			Root = RootNode;
			Nodes = new List<ConversationNode> ();
			Links = new List<ConversationLink> ();
		}

		public void AddLink(ConversationNode parentnode, ConversationLink link)
		{
			parentnode.Links.Add (link);
			Links.Add (link);
			Nodes.Add (link.nextNode);
		}
	}

	public class ConversationNode
	{
		//public static Dictionary<string, ConversationNode> NodeTable;

		public List<ConversationLink> Links;

		public string Line;

		public int IndentLvl;

		public ConversationNode( string line)
		{
			Links = new List<ConversationLink>();
			Line = line;
		}

		public bool IsLeaf
		{
			get
			{
				if (Links == null || Links.Count == 0)
					return true;
				return false;
			}
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

