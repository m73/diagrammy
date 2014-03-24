using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public class SaveButton : Button
	{
		public SaveButton() : base() 
		{
			this.Text = "Save";
			this.OnClientClick = "Diagram.save()";
		}

		protected override void OnClick (EventArgs e)
		{
			base.OnClick (e);
		}
	}
}

