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
			this.ID = "diagrammy-save-button";
			this.Text = "Save";
			this.OnClientClick = "Diagrammy.save()";
		}

		protected override void OnClick (EventArgs e)
		{
			base.OnClick (e);
		}
	}
}

