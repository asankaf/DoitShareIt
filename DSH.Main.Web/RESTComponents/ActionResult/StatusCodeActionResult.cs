using System.Web.Mvc;

namespace DSH.Main.Web.RESTComponents.ActionResult
{
    class StatusCodeActionResult : System.Web.Mvc.ActionResult
    {
        int statusCode;
        string statusDescription;

        public StatusCodeActionResult(int code)
        {
            statusCode = code;
        }

        public StatusCodeActionResult(int code, string description)
        {
            statusCode = code;
            statusDescription = description;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = statusCode;
            if (!string.IsNullOrEmpty(statusDescription))
            {
                response.StatusDescription = statusDescription;
            }
        }
    }
}
