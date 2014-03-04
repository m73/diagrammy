using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;

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
			writer.Write("<div><p>Some new text.</p></div>");
			string scriptName = "diagrammy.diagrammy.js";
			Type scriptType = typeof(Diagrammy);
			ClientScriptManager cs = Page.ClientScript;
			writer.Write ("<script src='"+ cs.GetWebResourceUrl (scriptType, scriptName) + "'></script>");
		}

		protected override HtmlTextWriterTag TagKey
		{
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}
}
