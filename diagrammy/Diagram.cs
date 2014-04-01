using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	/// Top tier Diagram component that can be placed in a page.
	/// </summary>
    public class Diagram : WebControl
	{
		/// <summary>
		/// Sends back altered diagram state in ajax manner.
		/// </summary>
		public SaveButton save;
		private DiagramProperties Properties; // Serializable object presenting all of its data.

		public Diagram(Object objectDiagram = null) : base() 
		{
			this.CreateControlCollection ();
			this.save = new SaveButton ();
			this.Properties = new DiagramProperties ();
			if (objectDiagram != null) {
				this.BuildFromObject(objectDiagram);
			}
		}

		/// <summary>
		/// Usage: d.AddNode(n)
		/// Before:
		/// After: n is in d. d contains n's NodeType if it was not there already.
		/// </summary>
		/// <param name="node">Node.</param>
		public void AddNode(Node Node) 
		{ 
			string NodeTypeID = Node.NodeType.ID;
			string NodeID = Node.Properties.ID;
			if (!this.Properties.NodeTypes.ContainsKey (NodeTypeID)) 
			{
				this.Properties.NodeTypes.Add (NodeTypeID, Node.NodeType);
			}
			if (!this.Properties.Nodes.ContainsKey (NodeID)) 
			{
				this.Properties.Nodes.Add (NodeID, Node.Properties);
				this.Controls.Add (Node);
			}
		}

		/// <summary>
		/// Make a json string representing the diagram. 
		/// </summary>
		private string ToJson() {
			return JsonConvert.SerializeObject(this.Properties);
		}

		/// <summary>
		/// Make this diagram mirror the diagram represented by the json string.
		/// </summary>
		private void BuildFromObject(Object objectDiagram) {

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

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
		}

		// For setting properties of component.
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
		}

		protected override void OnPreRender(EventArgs e) 
		{
			base.OnPreRender (e);
		}

		protected override void AddAttributesToRender (HtmlTextWriter writer)
		{
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			string diaScriptName = "diagrammy.js.diagrammy.js";
			string plumbScriptName = "diagrammy.lib.jquery.jsPlumb-1.5.5.js";
			Type scriptType = typeof(Diagram);
			ClientScriptManager cs = Page.ClientScript;
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.css.demochart.css")+"'/>");
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.css.nodes.css")+"'/>");
			writer.AddAttribute (HtmlTextWriterAttribute.Class, "demo flowchart-demo");
			writer.AddAttribute (HtmlTextWriterAttribute.Id, "flowchart-demo");
			writer.RenderBeginTag (HtmlTextWriterTag.Div);
			this.RenderChildren (writer);
			writer.RenderEndTag ();
			this.save.RenderControl (writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, "diagrammy-status");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();

			// Libraries
			writer.Write("<script src='" + cs.GetWebResourceUrl(scriptType, "diagrammy.lib.jquery-2.1.0.js") + "'></script>");
			writer.Write("<script src='" + cs.GetWebResourceUrl(scriptType, "diagrammy.lib.jquery.ui.touch-punch.js") + "'></script>");
			writer.Write("<script src='" + cs.GetWebResourceUrl(scriptType, plumbScriptName) + "'></script>");

			// Diagrammy scripts
			writer.Write ("<script src='" + cs.GetWebResourceUrl (scriptType, diaScriptName) + "'></script>");
			writer.Write ("<script type='text/javascript'>Diagrammy.data = " + this.ToJson() + ";</script>");
		}

		protected override HtmlTextWriterTag TagKey
		{
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}

	public class DiagramProperties
	{
		public Dictionary<string, NodeType> NodeTypes;
		public Dictionary<string, NodeProperties> Nodes;

		public DiagramProperties() 
		{
			this.NodeTypes = new Dictionary<string, NodeType> ();
			this.Nodes = new Dictionary<string, NodeProperties> ();
		}
	}
}
