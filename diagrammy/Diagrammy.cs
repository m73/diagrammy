using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public class Diagrammy : WebControl
	{
		protected override void RenderContents(HtmlTextWriter writer) {

			writer.Write("<div><p>Some text.</p></div>");
		}
	}
}
