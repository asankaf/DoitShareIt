using System.Net;
using DSH.Main.Web.RESTComponents.Serializer;


namespace DSH.Main.Web.RESTComponents.ActionResult
{
    class JsonActionResult : RestfulActionResultBase
    {
        protected override IRestfulSerializer Serializer
        {
            get { return Bucket.Instance.Resolve<JsonSerializer>(); }
        }

        public JsonActionResult(object data, ContentTypes contentType, HttpStatusCode statusCode)
            : base(data, contentType, statusCode)
        { 
            
        }
    }
}
