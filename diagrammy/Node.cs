using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;

	// A node instance which can be added to a diagram.
	public class Node : WebControl {
		public NodeType type; // Rules and styles for this particular node.
		public HashSet<Node> outputTo; // Nodes that this node has connected to.
		public HashSet<Node> inputFrom; // Nodes from which this node has been connected to.

		public Node(NodeType type) : base() {
			this.type = type;
			outputTo = new HashSet<Node>(); // Nodes that this node has connected to.
			inputFrom = new HashSet<Node>(); // Nodes from which this node has been connected to.
		}

		// Add classes and other attributes to node here.
		protected override void AddAttributesToRender(HtmlTextWriter writer) 
		{
			string classes = "diagrammy-"+this.type.shape;

			writer.AddAttribute (HtmlTextWriterAttribute.Class, classes);
			writer.AddAttribute (HtmlTextWriterAttribute.Id, "somediv");
			writer.AddAttribute (HtmlTextWriterAttribute.Style, "background: " + this.type.color);
		}

		// What is rendered inside this Node (i.e. inside the div it represents).
		protected override void RenderContents(HtmlTextWriter writer) 
		{
			writer.Write (this.type.label);
		}

		protected override HtmlTextWriterTag TagKey {
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}
}

