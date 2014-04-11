using NUnit.Framework;
using System;

namespace unit_tests
{
	using diagrammy;
	using Newtonsoft.Json;
	using System.Collections.Generic;

	[TestFixture ()]
	public class DiagramTest
	{
		[Test ()]
		public void BuildFromJsonTest ()
		{
			Diagram dia = new Diagram();

			// Configure in some way.
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

			// Convert
			string jsonDia = dia.ToJson();
			Diagram diaReplica = Diagram.BuildFromJson(jsonDia);

			//Test dia vs diaReplica (can be made more thorough)
			Assert.AreEqual(diaReplica.Nodes.Count, dia.Nodes.Count);
			Assert.AreEqual(diaReplica.NodeTypes.Count, dia.NodeTypes.Count);
			Assert.AreNotSame(diaReplica, dia);
		}
	}
}

