using System;
using System.IO;

namespace DSH.Main.Web.RESTComponents.ModelBinder
{
    abstract class RestfulModelBinderBase : IBucketResource
    {
        public abstract object Deserialize(Stream stream, Type type);
        public abstract string ContentType { get; }

        protected Bucket Bucket 
        {
            get { return Bucket.Instance; }
        }

        public abstract bool IsReusable
        {
            get;
        }
    }
}
