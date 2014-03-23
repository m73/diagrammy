using System;

namespace diagrammy
{
	using Newtonsoft.Json;
	using System.IO;
	using System.Text;

	public class JsonParser
	{
		/*
		public static string toJson<T>(T obj) where T : class
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
			using (MemoryStream stream = new MemoryStream())
			{
				serializer.WriteObject(stream, obj);
				return Encoding.Default.GetString(stream.ToArray());
			}
		}
		*/
	}
}

