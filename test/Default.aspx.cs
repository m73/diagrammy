
namespace test
{
	using System;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.Services;
	using diagrammy;

	public partial class Default : System.Web.UI.Page
	{
		/// <summary>
		/// A diagram that has been saved with save button or is initial startup diagram.
		/// </summary>
		public static Diagram dia;	

		/// <summary>
		/// An API usage example.
		/// </summary>
		private static void BuildSampleDiagram() {

			dia = new Diagram ();

			// Configure.
			NodeType electroMeter = new NodeType ("circle", "red");
			NodeType building = new NodeType ("square", "grey");
			electroMeter.AddIORule (building, "input", "one");
			building.AddIORule (electroMeter, "output", "one");
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
		}

		protected override void OnInit(EventArgs e) {

			base.OnInit(e);

			BuildSampleDiagram(); 

			Button load = new Button();
			load.Text = "Load";
			load.Click += onLoadClick;

			this.Controls.AddAt(0, dia);
			this.Form.Controls.AddAt(0, load);
		}

		protected void onLoadClick(object sender, EventArgs e) {

			// Swap out current diagram for the one saved before.
			this.Controls.AddAt(0, dia);
		}

		// Name of called method should be provided to the diagram constructor.
		[WebMethod]
		public static string DelegateDiagramData(string diagram) {

			// Save the diagram somewhere.
			dia = new Diagram(diagram);

			// Success response message.
			return "The diagram has been saved.";
		}
	}
}
