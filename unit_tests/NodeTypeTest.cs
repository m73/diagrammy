using NUnit.Framework;
using System;

namespace unit_tests
{
	using diagrammy;

	[TestFixture ()]
	public class NodeTypeTest
	{
		[Test ()]
		public void IORuleTest ()
		{
			NodeType NodeType1 = new NodeType("rectangle", "yellow");
			NodeType NodeType2 = new NodeType("circle", "brown");
			NodeType1.AddIORule(NodeType2, "input", "one");

			Assert.IsTrue(NodeType1.inputs.ContainsKey(NodeType2.GetHashCode()));
			Assert.IsTrue(NodeType1.inputs.ContainsValue("one"));
			NodeType1.RemoveIORule(NodeType2, "input");
			Assert.IsFalse(NodeType1.inputs.ContainsKey(NodeType2.GetHashCode()));
			Assert.IsFalse(NodeType1.inputs.ContainsValue("one"));
		}
	}
}

