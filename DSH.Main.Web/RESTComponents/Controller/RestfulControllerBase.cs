using System.Net;
using DSH.Main.Web.RESTComponents.ActionResult;

namespace DSH.Main.Web.RESTComponents.Controller
{
    public abstract class RestfulControllerBase : System.Web.Mvc.Controller
    {
        #region ActionResults

        protected System.Web.Mvc.ActionResult Status(HttpStatusCode statusCode)
        {
            return new StatusCodeActionResult((int)statusCode);
        }

        protected System.Web.Mvc.ActionResult Status(HttpStatusCode statusCode, string description)
        {
            return new StatusCodeActionResult((int)statusCode, description);
        }

        protected System.Web.Mvc.ActionResult REST(object data)
        {
            return REST(data, ContentTypes.Json);
        }

        protected System.Web.Mvc.ActionResult REST(object data, ContentTypes contentType)
        {
            return REST(data, contentType, HttpStatusCode.OK);
        }

        protected System.Web.Mvc.ActionResult REST(object data, ContentTypes contentType, HttpStatusCode statusCode)
        {
            return RestfulActionResultFactory.Result(data, contentType, statusCode);
        }

        #endregion
    }
}
