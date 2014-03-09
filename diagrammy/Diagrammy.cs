using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;

	public class Diagrammy : WebControl
	{
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

		protected override void RenderContents(HtmlTextWriter writer)
		{
			string diaScriptName = "diagrammy.diagrammy.js";
			string plumbScriptName = "diagrammy.jquery.jsPlumb-1.5.5.js";
			string demoScriptName = "diagrammy.demo.js";
			Type scriptType = typeof(Diagrammy);
			ClientScriptManager cs = Page.ClientScript;
			writer.Write ("<link rel='stylesheet' href='"+cs.GetWebResourceUrl(scriptType, "diagrammy.demo.css")+"'/>");
			writer.Write("<div class='demo flowchart-demo' id='flowchart-demo'>");
			writer.Write("<div class='window' id='flowchartWindow1'><strong>1</strong><br/><br/></div>");
			writer.Write("<div class='window' id='flowchartWindow2'><strong>1</strong><br/><br/></div>");
			writer.Write("<div class='window' id='flowchartWindow3'><strong>1</strong><br/><br/></div>");
			writer.Write("<div class='window' id='flowchartWindow4'><strong>1</strong><br/><br/></div>");                    
			writer.Write("</div>");
			                        
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
	public class Node {
		// Node types(string) allowed to connect to/from this node and how many(int) of them can connect/be connected.
		// If int is n, and n > 0, 1 to n can be connected. Otherwise any number can be connected.
		public Dictionary<string, int> inputs;
		public Dictionary<string, int> outputs;
		public string label; // Label shown on the node on the diagram.
		public string type; // Type of the node. Used as an identifier. (may be used internally and not changed by API user)
		public string shape; // Shape as shown on diagram. Can be "circle", "square" or "rectangle".
		public string color; // Color of node. Can be any CSS color name or hex value, e.g. "Red", "#DC143C".

		// Used in the control's RenderContents to place a div in the DOM representingthe node.
		public void RenderContents(HtmlTextWriter writer) {
			writer.Write("<div> </div>"); // Needs configuring by Node's properties.
		}
	}
}
