using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;

	/// <summary>
	/// A node instance which can be added to a diagram.
	/// </summary>
	public class Node : WebControl
	{
		public NodeType NodeType; // Rules and styles for this particular node.
		public NodeProperties Properties;

		public Node(string label, NodeType NodeType) : base() 
		{
			this.NodeType = NodeType;
			this.Properties = new NodeProperties (label, NodeType.GetHashCode());
		}

		/// <summary>
		/// Make connection between this node to another node.
		/// </summary>
		/// <param name="Node">Node.</param>
		public void Connect(Node Node) {
			this.Properties.Out.Add (Node.Properties.GetHashCode());
			Node.Properties.In.Add (this.Properties.GetHashCode());
		}

		/// <summary>
		/// Adds id, classes and other styles to node.
		/// </summary>
		/// <param name="writer">Writer.</param>
		protected override void AddAttributesToRender(HtmlTextWriter writer) 
		{
			string classes = "diagrammy-"+this.NodeType.shape;

			writer.AddAttribute (HtmlTextWriterAttribute.Class, classes);
			writer.AddAttribute (HtmlTextWriterAttribute.Id, "diagrammy-" + this.GetHashCode());
			writer.AddAttribute (HtmlTextWriterAttribute.Style, "background: " + this.NodeType.color);
		}

		// What is rendered inside this Node (i.e. inside the div it represents).
		protected override void RenderContents(HtmlTextWriter writer) 
		{
			writer.Write (this.Properties.label);
		}

		protected override HtmlTextWriterTag TagKey {
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}

	/// <summary>
	/// Properties of a node which may be used on the client side.
	/// </summary>
	public class NodeProperties {

		public NodeProperties(string label, int NodeTypeHash) {
			this.Out = new HashSet<int>();
			this.In = new HashSet<int> ();
			this.label = label;
			this.type = NodeTypeHash;
		}

		public HashSet<int> In;
		public HashSet<int> Out;
		public string label;
		public int type;
	}
}

