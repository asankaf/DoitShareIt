using System;
using System.Net;
using System.Web.Mvc;
using DSH.Main.Web.RESTComponents.Serializer;

namespace DSH.Main.Web.RESTComponents.ActionResult
{
    abstract class RestfulActionResultBase : System.Web.Mvc.ActionResult
    {
        protected abstract IRestfulSerializer Serializer { get; }

        protected object data;
        protected ContentTypes contentType;
        protected HttpStatusCode statusCode;

        public RestfulActionResultBase(object data, ContentTypes contentType, HttpStatusCode statusCode)
        {
            this.data = data;
            this.contentType = contentType;
            this.statusCode = statusCode;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            response.StatusCode = (int)statusCode;
            
            //Alternate response type passed in the url by the client
            string alt = context.RouteData.Values["alt"] as string;    
            if (!string.IsNullOrEmpty(alt))
            {
                if (alt.ToLower().Contains("xml"))
                {
                    contentType = ContentTypes.Xml;
                }
                else if (alt.ToLower().Contains("json"))
                {
                    contentType = ContentTypes.Json;
                }
            }

            if (contentType != ContentTypes.Null)
            {
                response.ContentType = string.Format(
                    RestfulContent.ContentTypeApplication,
                    contentType.ToString().ToLower());
            }

            if (data != null)
            {
                if (contentType == ContentTypes.Json ||
                    contentType == ContentTypes.Xml)
                {
                    //Add model response type so clients know how to deserialize the response
                    response.AddHeader(RestfulContent.Expect, data
                        .GetType()
                        .AssemblyQualifiedName);
                }

                try
                {
                    var stream = Serializer.Serialize(data);
                    response.BinaryWrite(stream.ToArray());
                }
                catch (Exception e)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusDescription = RestfulContent.ResponseSerializationFailed;
                    response.ContentType = string.Empty;
                }
            }   
        }
    }
}
