using NUnit.Framework;
using System;

namespace unit_tests
{
	using diagrammy;

	[TestFixture ()]
	public class NodeTest
	{
		[Test ()]
		public void ConnectTest ()
		{
			Node node1 = new Node("NiceNode", new NodeType("circle", "purple"));
			Node node2 = new Node("BadNode", new NodeType("square", "red"));
			node1.Connect(node2);
			Assert.IsTrue(node1.Properties.Out.Contains(node2.Properties.ID));
			Assert.IsTrue(node2.Properties.In.Contains(node1.Properties.ID));
			node1.Disconnect(node2);
			Assert.IsFalse(node1.Properties.Out.Contains(node2.Properties.ID));
			Assert.IsFalse(node2.Properties.In.Contains(node1.Properties.ID));
		}
	}
}

