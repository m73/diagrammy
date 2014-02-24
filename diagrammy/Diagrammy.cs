using System;
//blaha
namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public class Diagrammy : WebControl
	{
		protected override void RenderContents(HtmlTextWriter writer) {

			writer.Write("<div><p>Some new text.</p></div>");
		}

		protected override HtmlTextWriterTag TagKey
		{
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}
}
