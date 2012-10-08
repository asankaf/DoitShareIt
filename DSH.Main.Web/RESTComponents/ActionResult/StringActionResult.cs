using System.Net;
using DSH.Main.Web.RESTComponents.Serializer;

namespace DSH.Main.Web.RESTComponents.ActionResult
{
    class StringActionResult : RestfulActionResultBase
    {
        protected override IRestfulSerializer Serializer
        {
            get { return Bucket.Instance.Resolve<StringSerializer>(); }
        }

        public StringActionResult(object data, ContentTypes contentType, HttpStatusCode statusCode)
            : base(data, contentType, statusCode)
        { 
            
        }
    }
}
