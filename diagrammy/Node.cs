using System;

namespace diagrammy
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using Newtonsoft.Json;

	/// <summary>
	/// Nodes of a Diagram, basically styled divs with a NodeType and properties such as label.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class Node : WebControl
	{
		/// <summary>
		/// Type of the node which controls its behavior.
		/// </summary>
		public NodeType NodeType;

		/// <summary>
		/// The ID for the next instance of this class.
		/// </summary>
		private static int NextID;
		private string _id;
		public override string ID {
			get { return _id; }
		}

		// Properties appearing in this object's serialized equivalent.
		[JsonProperty]
		public HashSet<string> In;
		[JsonProperty]
		public HashSet<string> Out;
		[JsonProperty]
		public string Label;
		[JsonProperty]
		public string NodeTypeID;

		public Node(string Label, NodeType nodeType) : base() 
		{
			this.NodeType = nodeType;
			this.Label = Label;
			this.NodeTypeID = this.NodeType.ID;
			this.Out = new HashSet<string>();
			this.In = new HashSet<string> ();
			this._id = "diagrammy-node" + NextID.ToString();
			NextID++;
		}

		[OnDeserializing]
		internal void OnDeserializing(StreamingContext context) {
			
		} 

		/// <summary>
		/// Make connection between this node to another node.
		/// </summary>
		public void Connect(Node node) {
			this.Out.Add (node.ID);
			node.In.Add (this.ID);
		}

		/// <summary>
		/// Remove connection between this node to another node.
		/// </summary>
		public void Disconnect(Node node) {
			this.Out.Remove(node.ID);
			node.In.Remove(this.ID);
		}

		/// <summary>
		/// </summary>
		/// <param name="writer">Writer.</param>
		protected override void AddAttributesToRender(HtmlTextWriter writer) 
		{
			string classes = "diagrammy-"+this.NodeType.Shape;

			writer.AddAttribute (HtmlTextWriterAttribute.Class, classes);
			writer.AddAttribute (HtmlTextWriterAttribute.Id, this.ID);
			writer.AddAttribute (HtmlTextWriterAttribute.Style, "background: " + this.NodeType.Color);
		}

		// What is rendered inside this Node (i.e. inside the div it represents).
		protected override void RenderContents(HtmlTextWriter writer) 
		{
			writer.Write (this.Label);
		}

		protected override HtmlTextWriterTag TagKey {
			get {
				return HtmlTextWriterTag.Div;
			}
		}
	}

}

