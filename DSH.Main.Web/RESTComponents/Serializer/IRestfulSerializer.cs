using System;
using System.IO;

namespace DSH.Main.Web.RESTComponents.Serializer
{
    interface IRestfulSerializer : IDisposable
    {
        object Deserialize(Stream stream, Type type);
        MemoryStream Serialize(object data);
    }
}
