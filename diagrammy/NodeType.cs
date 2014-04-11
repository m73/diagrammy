using System;

namespace diagrammy
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
	
	/// <summary>
	/// Defines rules (behavior) and styles for Nodes that contain it as their NodeType. <br/>
	/// Its properties are serializable for conversion to json.
	/// </summary>
	[JsonObject(IsReference = true)]
	public class NodeType 
	{
		/// <summary>
		/// NodeTypes allowed to connect to this NodeType (int is the corresponding NodeType's id value),<br/>
		/// and how many such connections can exist (string is "one" or "many").
		/// </summary>
		public Dictionary<string, string> Inputs;

		/// <summary>
		/// NodeTypes this NodeType is allowed to connect to (int is the corresponding NodeType's id value),<br/>
		/// and how many such connections can exist (string is "one" or "many").
		/// </summary>
		public Dictionary<string, string> Outputs;

		/// <summary>
		/// Shape as shown on diagram. Can be "circle", "square" or "rectangle".
		/// </summary>
		public string Shape;

		/// <summary>
		/// Color of node. Can be any CSS color name or hex value, e.g. "Red", "#DC143C".
		/// </summary>
		public string Color;

		/// <summary>
		/// Next ID that a new instance will get.
		/// </summary>
		private static int NextID = 0;

		/// <summary>
		/// Unique ID for this NodeType.
		/// </summary>
		[JsonIgnore]
		public string ID;

		public NodeType(string shape, string color) {
			this.Shape = shape;
			this.Color = color;
			Inputs = new Dictionary<string, string> ();
			Outputs = new Dictionary<string, string> ();
			this.ID = "diagrammy-nodetype" + NextID.ToString();
			NextID++;
		}

		/// <summary>
		/// Puts in a new specified rule (ruleType, "input" or "output") for a specified NodeType,<br/> 
		/// and how many can connect ("many" or "one"). Overwrites if a rule for nodeType already exists.
		/// </summary>
		public void AddIORule(NodeType nodeType, string ruleType = "output", string amount = "many")
		{
			Dictionary<string, string> puts = ruleType == "input" ? this.Inputs : this.Outputs;

			if (puts.ContainsKey(nodeType.ID))
			{
				puts.Remove(nodeType.ID);
			}
			puts.Add(nodeType.ID, amount);
		}

		/// <summary>
		/// Removes the IO rule for specified nodeType and ruleType.
		/// </summary>
		public void RemoveIORule(NodeType nodeType, string ruleType = "output") {
			Dictionary<string, string> puts = ruleType == "input" ? this.Inputs : this.Outputs;
			puts.Remove(nodeType.ID);
		}
	}
}

