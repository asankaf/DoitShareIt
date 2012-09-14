using System;
using System.Text;
using System.IO;

namespace DSH.Main.Web.RESTComponents.Serializer
{
    class StringSerializer : IRestfulSerializer, IBucketResource
    {
        public object Deserialize(System.IO.Stream stream, Type type)
        {
            using (TextReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public System.IO.MemoryStream Serialize(object data)
        {
            if (!(data is string))
            {
                Errors.InvalidObjectForSerializer(data.GetType().Name);
            }

            return new MemoryStream(Encoding.Default.GetBytes(data as string));
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
