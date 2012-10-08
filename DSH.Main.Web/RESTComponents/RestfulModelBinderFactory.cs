using DSH.Main.Web.RESTComponents.ModelBinder;

namespace DSH.Main.Web.RESTComponents
{
    static class RestfulModelBinderFactory
    {
        public static RestfulModelBinderBase GetBinder(string contentType)
        {
            if (contentType.Contains("xml"))
            {
                return DSH.Main.Web.RESTComponents.Bucket.Instance.Resolve<XmlModelBinder>();
            }
            else if (contentType.Contains("json"))
            {
                return DSH.Main.Web.RESTComponents.Bucket.Instance.Resolve<JsonModelBinder>();
            }

            return null;
        }
    }
}
