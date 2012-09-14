using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DSH.Main.Web.RESTComponents.Serializer
{
    class JsonSerializer : IRestfulSerializer, IBucketResource
    {
        public object Deserialize(Stream stream, Type type)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
            stream.Position = 0;
            return serializer.ReadObject(stream); 
        }

        public System.IO.MemoryStream Serialize(object data)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());

            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, data);
            return stream;
        }

        public void Dispose()
        {
            
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
