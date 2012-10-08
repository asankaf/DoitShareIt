using System;
using System.IO;

namespace DSH.Main.Web.RESTComponents.Serializer
{
    class XmlSerializer : IRestfulSerializer, IBucketResource
    {
        public object Deserialize(System.IO.Stream stream, Type type)
        {
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(type);
            stream.Position = 0;
            return serializer.Deserialize(stream);
        }

        public System.IO.MemoryStream Serialize(object data)
        {
            Type type = data.GetType();

            if (!type.IsSerializable)
            {
                throw Errors.ObjectNotSerializable();
            }

            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(data.GetType());

            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, data);
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
