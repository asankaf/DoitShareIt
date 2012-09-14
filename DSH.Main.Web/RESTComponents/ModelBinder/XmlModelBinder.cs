using System;
using System.IO;
using DSH.Main.Web.RESTComponents.Serializer;

namespace DSH.Main.Web.RESTComponents.ModelBinder
{
    class XmlModelBinder : RestfulModelBinderBase
    {
        public override object Deserialize(Stream stream, Type type)
        {
            var serializer = Bucket.Resolve<XmlSerializer>();
            return serializer.Deserialize(stream, type);
        }

        public override string ContentType
        {
            get { return "xml"; }
        }

        public override bool IsReusable
        {
            get { return true; }
        }
    }
}
