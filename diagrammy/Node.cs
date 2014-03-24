using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;

	/// <summary>
	/// Nodes of a Diagram, basically styled divs with a NodeType and properties such as label.
	/// </summary>
	public class Node : WebControl
	{
		/// <summary>
		/// Type of the node which controls its behavior.
		/// </summary>
		public NodeType NodeType;

		/// <summary>
		/// Serializable properties of the node, i.e. what gets converted to json.
		/// </summary>
		public NodeProperties Properties;

		public Node(string label, NodeType NodeType) : base() 
		{
			this.NodeType = NodeType;
			this.Properties = new NodeProperties (label, NodeType.GetHashCode()); // Contain only number reference on json side.
		}

		/// <summary>
		/// Make connection between this node to another node.
		/// </summary>
		public void Connect(Node node) {
			this.Properties.Out.Add (node.Properties.GetHashCode());
			node.Properties.In.Add (this.Properties.GetHashCode());
		}

		/// <summary>
		/// Remove connection between this node to another node.
		/// </summary>
		public void Disconnect(Node node) {
			this.Properties.Out.Remove(node.Properties.GetHashCode());
			node.Properties.In.Remove(this.Properties.GetHashCode());
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

