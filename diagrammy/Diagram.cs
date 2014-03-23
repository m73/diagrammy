using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;
	using Newtonsoft.Json;
	
    public class Diagram : WebControl
	{
		public SaveButton save;
		public LoadButton load;
		private string JsonData = "Diagrammy.data = "; // javascript definition of Properties.
		private DiagramProperties Properties; // Serializable object presenting all of its data.

		public Diagram() : base() 
		{
			this.CreateControlCollection ();
			this.save = new SaveButton ();
			this.load = new LoadButton ();
			this.Properties = new DiagramProperties ();
		}

		/// <summary>
		/// Usage: d.AddNode(n)
		/// Before:
		/// After: n is in d. d contains n's NodeType if it was not there already.
		/// </summary>
		/// <param name="node">Node.</param>
		public void AddNode(Node Node) 
		{
			int NodeTypeHash = Node.NodeType.GetHashCode ();
			int NodeHash = Node.Properties.GetHashCode ();
			if (!this.Properties.NodeTypes.ContainsKey (NodeTypeHash)) 
			{
				this.Properties.NodeTypes.Add (NodeTypeHash, Node.NodeType);
			}
			if (!this.Properties.Nodes.ContainsKey (NodeHash)) 
			{
				this.Properties.Nodes.Add (NodeHash, Node.Properties);
				this.Controls.Add (Node);
			}
		}

		/// <summary>
		/// Usage: d.BuildJsonData().
		/// Before:
		/// After: d.JsonData contains all necessary diagram information of d
		/// 	   to build the diagram in the browser.
		/// </summary>
		private void BuildJsonData() {
			this.JsonData += JsonConvert.SerializeObject(this.Properties) + ";";
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
            BuildJsonData();
		}

		protected override void AddAttributesToRender (HtmlTextWriter writer)
		{
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			string diaScriptName = "diagrammy.js.diagrammy.js";
			string plumbScriptName = "diagrammy.lib.jquery.jsPlumb-1.5.5.js";
			string demoScriptName = "diagrammy.js.demo.js";
			Type scriptType = typeof(Diagram);
			ClientScriptManager cs = Page.ClientScript;
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.css.demo.css")+"'/>");
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.css.demo-all.css")+"'/>");
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.css.nodes.css")+"'/>");
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
			// Add json object here.
			writer.Write ("<script src='" + cs.GetWebResourceUrl (scriptType, diaScriptName) + "'></script>");
			writer.Write ("<script type='text/javascript'>" + this.JsonData + "</script>");
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
		public Dictionary<int, NodeType> NodeTypes;
		public Dictionary<int, NodeProperties> Nodes;

		public DiagramProperties() 
		{
			this.NodeTypes = new Dictionary<int, NodeType> ();
			this.Nodes = new Dictionary<int, NodeProperties> ();
		}
	}
}
