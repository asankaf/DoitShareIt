using System.Net;
using DSH.Main.Web.RESTComponents.ActionResult;

namespace DSH.Main.Web.RESTComponents
{
    static class RestfulActionResultFactory
    {
        public static System.Web.Mvc.ActionResult Result(object data, DSH.Main.Web.RESTComponents.ContentTypes contentType, HttpStatusCode statusCode)
        {
            switch (contentType)
            { 
                case DSH.Main.Web.RESTComponents.ContentTypes.Html:
                case DSH.Main.Web.RESTComponents.ContentTypes.Text:
                case DSH.Main.Web.RESTComponents.ContentTypes.FormEncoded:
                    return new StringActionResult(data, contentType, statusCode);
                case DSH.Main.Web.RESTComponents.ContentTypes.Binary:
                    return new BinaryActionResult(data, contentType, statusCode);
                case DSH.Main.Web.RESTComponents.ContentTypes.Xml:
                    return new XmlActionResult(data, contentType, statusCode);
                case DSH.Main.Web.RESTComponents.ContentTypes.Json:
                    return new JsonActionResult(data, contentType, statusCode);
                default:
                    throw Errors.ContentTypeNotSupported(contentType);
            }
        }
    }
}
