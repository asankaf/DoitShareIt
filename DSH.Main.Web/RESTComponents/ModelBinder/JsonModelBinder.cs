using System;
using System.IO;
using DSH.Main.Web.RESTComponents.Serializer;

namespace DSH.Main.Web.RESTComponents.ModelBinder
{
    class JsonModelBinder : RestfulModelBinderBase
    {
        public override object Deserialize(Stream stream, Type type)
        {
            var serializer = Bucket.Resolve<JsonSerializer>();
            return serializer.Deserialize(stream, type);
        }

        public override string ContentType
        {
            get { return "json"; }
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
