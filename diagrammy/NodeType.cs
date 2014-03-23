using System;

namespace diagrammy
{
	using System.Collections.Generic;

	// An instance of this class defines rules and style of a single diagram node
	// which gets connected to other nodes.
	public class NodeType {
		// Node allowed to connect to/from this node and how many(int) of them can connect/be connected.
		// If int is n, and n > 0, 1 to n can be connected. Otherwise any number can be connected.
		public Dictionary<int, string> inputs;
		public Dictionary<int, string> outputs;
		public string label; // Label shown on the node on the diagram.
		public string shape; // Shape as shown on diagram. Can be "circle", "square" or "rectangle".
		public string color; // Color of node. Can be any CSS color name or hex value, e.g. "Red", "#DC143C".

		public NodeType(string shape, string color) {
			this.shape = shape;
			this.color = color;
			inputs = new Dictionary<int, string> ();
			outputs = new Dictionary<int, string> ();
		}

		/// <summary>
		/// Puts in a new input rule for a specified NodeType: "many" or "one".
		/// </summary>
		/// <remarks>
		/// Usage: x = a.InputRule(b, m)<br/>
		/// Before:<br/>
		/// After: If m is supplied and is not "none", b is now in a.inputs and x = m. If m was supplied or is "none",<br/>
		/// 	   x is the amount in a.inputs associated with b or "none" if it wasn't found.<br/>
		/// </remarks>
		/// <returns>The rule.</returns>
		/// <param name="nt">Nt.</param>
		/// <param name="amount">Amount.</param>
		public string InputRule(NodeType NodeType, string amount = "none") 
		{
			int key = NodeType.GetHashCode ();
			if (amount == "none") 
			{
				string foundAmount;
				this.inputs.TryGetValue (key, out foundAmount);
				return foundAmount;
			} 
			else if (!this.inputs.ContainsKey(key))
			{
				this.inputs.Add (key, amount);
			}
			return amount;
		}

		/// <summary>
		/// Puts in a new output rule for a specified NodeType: "many" or "one".
		/// </summary>
		/// <remarks>
		/// Usage: x = a.OutputRule(b, m)<br/>
		/// Before:<br/>
		/// After: If m is supplied and is not "none", b is now in a.outputs and x = m. If m was supplied or is "none",<br/>
		/// 	   x is the amount in a.outputs associated with b or "none" if it wasn't found.<br/>
		/// </remarks>
		/// <returns>The rule.</returns>
		/// <param name="nt">Nt.</param>
		/// <param name="amount">Amount.</param>
		public string OutputRule(NodeType nt, string amount = "none") 
		{
			int key = nt.GetHashCode ();
			if (amount == "none") 
			{
				string foundAmount;
				this.outputs.TryGetValue (key, out foundAmount);
				return foundAmount;
			} 
			else if (!this.inputs.ContainsKey(key))
			{
				this.outputs.Add (key, amount);
			}
			return amount;
		}
	}
}

