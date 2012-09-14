using System.Net;
using DSH.Main.Web.RESTComponents.Serializer;

namespace DSH.Main.Web.RESTComponents.ActionResult
{
    class XmlActionResult : RestfulActionResultBase
    {
        protected override IRestfulSerializer Serializer
        {
            get { return Bucket.Instance.Resolve<XmlSerializer>(); }
        }

        public XmlActionResult(object data, ContentTypes contentType, HttpStatusCode statusCode)
            : base(data, contentType, statusCode)
        { 
            
        }
    }
}
