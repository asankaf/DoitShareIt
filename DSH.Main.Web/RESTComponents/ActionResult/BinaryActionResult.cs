using System;
using System.Net;
using DSH.Main.Web.RESTComponents.Serializer;

namespace DSH.Main.Web.RESTComponents.ActionResult
{
    class BinaryActionResult : RestfulActionResultBase
    {
        protected override IRestfulSerializer Serializer
        {
            get { throw new NotImplementedException(); }
        }

        public BinaryActionResult(object data, ContentTypes contentType, HttpStatusCode statusCode)
            : base(data, contentType, statusCode)
        { 
            
        }
    }
}
