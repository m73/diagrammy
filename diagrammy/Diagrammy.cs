using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;

	public class Diagrammy : WebControl
	{
		public SaveButton save;
		public LoadButton load;

		public Diagrammy() : base() 
		{
			this.CreateControlCollection ();
			this.save = new SaveButton ();
			this.load = new LoadButton ();
		}

		public void AddNode(Node node) 
		{
			this.Controls.Add(node);
		}

		protected override void RenderChildren(HtmlTextWriter writer) 
		{
			if (this.HasControls()) 
			{
				for(int i = 0; i < this.Controls.Count; i++) 
				{
					this.Controls[i].RenderControl(writer);
				}
			}         
		}

		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
		}

		protected override void OnPreRender(EventArgs e) 
		{
			string scriptName = "diagrammy.diagrammy.js";
			Type scriptType = this.GetType();
			ClientScriptManager cs = Page.ClientScript;
			cs.RegisterClientScriptResource (scriptType, scriptName);
			base.OnPreRender (e);
		}

		protected override void AddAttributesToRender (HtmlTextWriter writer)
		{
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			string diaScriptName = "diagrammy.diagrammy.js";
			string plumbScriptName = "diagrammy.jquery.jsPlumb-1.5.5.js";
			string demoScriptName = "diagrammy.demo.js";
			Type scriptType = typeof(Diagrammy);
			ClientScriptManager cs = Page.ClientScript;
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.demo.css")+"'/>");
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.demo-all.css")+"'/>");
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.nodes.css")+"'/>");
			writer.AddAttribute (HtmlTextWriterAttribute.Class, "demo flowchart-demo");
			writer.AddAttribute (HtmlTextWriterAttribute.Id, "flowchart-demo");
			writer.RenderBeginTag (HtmlTextWriterTag.Div);
			writer.Write("<div class='window' id='flowchartWindow1'><strong>1</strong><br/><br/></div>");
			writer.Write("<div class='window' id='flowchartWindow2'><strong>1</strong><br/><br/></div>");
			writer.Write("<div class='window' id='flowchartWindow3'><strong>1</strong><br/><br/></div>");
			writer.Write("<div class='window' id='flowchartWindow4'><strong>1</strong><br/><br/></div>");
			this.RenderChildren (writer);
			writer.RenderEndTag ();
			this.load.RenderControl (writer);
			this.save.RenderControl (writer);
			                        
			writer.Write("<script src='http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js'></script>");
			writer.Write("<script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/jquery-ui.min.js'></script>");
			writer.Write("<script src='" + cs.GetWebResourceUrl(scriptType, plumbScriptName) + "'></script>");
			writer.Write ("<script src='"+ cs.GetWebResourceUrl (scriptType, demoScriptName) + "'></script>");
			writer.Write ("<script src='"+ cs.GetWebResourceUrl (scriptType, diaScriptName) + "'></script>");
		}

		protected override HtmlTextWriterTag TagKey
		{
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}

	// An instance of this class defines rules and style of a single diagram node
	// which gets connected to other nodes.
	public class NodeType {
		// Node allowed to connect to/from this node and how many(int) of them can connect/be connected.
		// If int is n, and n > 0, 1 to n can be connected. Otherwise any number can be connected.
		public Dictionary<NodeType, int> inputs;
		public Dictionary<NodeType, int> outputs;
		public string label; // Label shown on the node on the diagram.
		public string type; // Type of the node. Used as an identifier. (may be used internally and not changed by API user)
		public string shape; // Shape as shown on diagram. Can be "circle", "square" or "rectangle".
		public string color; // Color of node. Can be any CSS color name or hex value, e.g. "Red", "#DC143C".

		public NodeType(string label, string shape, string color) {
			this.label = label;
			this.shape = shape;
			this.color = color;
		}
	}

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

	public class SaveButton : Button
	{
		public SaveButton() : base() 
		{
			this.Text = "Save";
			this.OnClientClick = "Diagram.save()";
		}
	}

	public class LoadButton : Button
	{
		public LoadButton() : base() 
		{
			this.Text = "Load";
			this.OnClientClick = "Diagram.save()";
		}
	}
}
