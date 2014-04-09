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

		public Node(NodeProperties nodeProperties, NodeType nodeType) : base() 
		{
			this.NodeType = nodeType;
			this.Properties = nodeProperties;
			this.Properties.NodeTypeID = this.NodeType.ID;
		}

		/// <summary>
		/// Make connection between this node to another node.
		/// </summary>
		public void Connect(Node node) {
			this.Properties.Out.Add (node.Properties.ID);
			node.Properties.In.Add (this.Properties.ID);
		}

		/// <summary>
		/// Remove connection between this node to another node.
		/// </summary>
		public void Disconnect(Node node) {
			this.Properties.Out.Remove(node.Properties.ID);
			node.Properties.In.Remove(this.Properties.ID);
		}

		/// <summary>
		/// </summary>
		/// <param name="writer">Writer.</param>
		protected override void AddAttributesToRender(HtmlTextWriter writer) 
		{
			string classes = "diagrammy-"+this.NodeType.Shape;

			writer.AddAttribute (HtmlTextWriterAttribute.Class, classes);
			writer.AddAttribute (HtmlTextWriterAttribute.Id, this.Properties.ID);
			writer.AddAttribute (HtmlTextWriterAttribute.Style, "background: " + this.NodeType.Color);
		}

		// What is rendered inside this Node (i.e. inside the div it represents).
		protected override void RenderContents(HtmlTextWriter writer) 
		{
			writer.Write (this.Properties.Label);
		}

		protected override HtmlTextWriterTag TagKey {
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}

	/// <summary>
	/// Properties of a node which may be used on the client side. It is meant to be unique for a given node.
	/// </summary>
	public class NodeProperties {

		public HashSet<string> In;
		public HashSet<string> Out;
		public string Label;
		public string NodeTypeID;
		private static int NextID;
		public string ID;

		public NodeProperties(string Label, string nodeTypeID = "") {
			this.Out = new HashSet<string>();
			this.In = new HashSet<string> ();
			this.Label = Label;
			this.NodeTypeID = nodeTypeID;
			this.ID = "diagrammy-node" + NextID.ToString();
			NextID++;
		}
	}
}

