using System;

namespace diagrammy
{
	using System.Collections.Generic;

	// An instance of this class defines rules and style of a single diagram node
	// which gets connected to other nodes.
	public class NodeType {
		// Node allowed to connect to/from this node and how many(int) of them can connect/be connected.
		// If int is n, and n > 0, 1 to n can be connected. Otherwise any number can be connected.
		public Dictionary<NodeType, int> inputs;
		public Dictionary<NodeType, int> outputs;
		public string label; // Label shown on the node on the diagram.
		public string type; // Type of the node. Used as an identifier. (may be used internally and not changed by API user)
		public string shape; // Shape as shown on diagram. Can be "circle", "square" or "rectangle".
		public string color; // Color of node. Can be any CSS color name or hex value, e.g. "Red", "#DC143C".

		public NodeType(string label, string shape, string color) {
			this.label = label;
			this.shape = shape;
			this.color = color;
		}
	}
}

