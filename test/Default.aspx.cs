
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
			Diagram dia = new Diagram ();

			// Configure the diagrammy.
			NodeType electroMeter = new NodeType ("circle", "red");
			NodeType building = new NodeType ("square", "grey");
			electroMeter.InputRule (building, "one");
			building.OutputRule (electroMeter, "one");
			Node eo = new Node ("M?", electroMeter);
			Node myHouse = new Node("My house", building);
			Node otherHouse = new Node ("Other house", building);
			Node eo2 = new Node ("M10", electroMeter);
			eo.Connect (myHouse);
			otherHouse.Connect (eo);
			dia.AddNode (eo);
			dia.AddNode (myHouse);
			dia.AddNode (eo2);
			dia.AddNode (otherHouse);

			this.Controls.Add (dia);
		}
	}
}
