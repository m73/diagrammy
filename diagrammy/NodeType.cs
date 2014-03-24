using System;

namespace diagrammy
{
	using System.Collections.Generic;
	
	/// <summary>
	/// Defines rules (behavior) and styles for Nodes that contain it as their NodeType. <br/>
	/// Its properties are serializable for conversion to json.
	/// </summary>
	public class NodeType 
	{
		/// <summary>
		/// NodeTypes allowed to connect to this NodeType (int is the corresponding NodeType's hash value),<br/>
		/// and how many such connections can exist (string is "one" or "many").
		/// </summary>
		public Dictionary<int, string> inputs;

		/// <summary>
		/// NodeTypes this NodeType is allowed to connect to (int is the corresponding NodeType's hash value),<br/>
		/// and how many such connections can exist (string is "one" or "many").
		/// </summary>
		public Dictionary<int, string> outputs;

		/// <summary>
		/// Shape as shown on diagram. Can be "circle", "square" or "rectangle".
		/// </summary>
		public string shape;

		/// <summary>
		/// Color of node. Can be any CSS color name or hex value, e.g. "Red", "#DC143C".
		/// </summary>
		public string color;

		public NodeType(string shape, string color) {
			this.shape = shape;
			this.color = color;
			inputs = new Dictionary<int, string> ();
			outputs = new Dictionary<int, string> ();
		}

		/// <summary>
		/// Puts in a new specified rule (ruleType, "input" or "output") for a specified NodeType,<br/> 
		/// and how many can connect ("many" or "one"). Overwrites if a rule for nodeType already exists.
		/// </summary>
		public void AddIORule(NodeType nodeType, string ruleType = "output", string amount = "many")
		{
			int key = nodeType.GetHashCode();
			Dictionary<int, string> puts = ruleType == "input" ? this.inputs : this.outputs;

			if (puts.ContainsKey(key))
			{
				puts.Remove(key);
			}
			puts.Add(key, amount);
		}

		/// <summary>
		/// Removes the IO rule for specified nodeType and ruleType.
		/// </summary>
		public void RemoveIORule(NodeType nodeType, string ruleType = "output") {
			int key = nodeType.GetHashCode();
			Dictionary<int, string> puts = ruleType == "input" ? this.inputs : this.outputs;
			puts.Remove(key);
		}
	}
}

