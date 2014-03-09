
namespace test
{
	using System;
	using System.Web;
	using System.Web.UI;
	using diagrammy;

	public partial class Default : System.Web.UI.Page
	{
		protected override void OnInit(EventArgs e) {
			base.OnInit(e);

			// Initialize diagrammy here and add to page.
			Diagrammy dia = new Diagrammy ();

			// Configure the diagrammy.

			this.Controls.Add (dia);
		}
	}
}
