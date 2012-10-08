using DSH.Main.Web.RESTComponents.ModelBinder;
using DSH.Main.Web.RESTComponents.Serializer;

namespace DSH.Main.Web.RESTComponents
{
    public sealed class RestfulMVC
    {
        DSH.Main.Web.RESTComponents.Bucket bucket;
        public void Init()
        {
            bucket = DSH.Main.Web.RESTComponents.Bucket.Instance;

            bucket.Register<XmlModelBinder>(() => new XmlModelBinder());
            bucket.Register<XmlSerializer>(() => new XmlSerializer());
            bucket.Register<JsonModelBinder>(() => new JsonModelBinder());
            bucket.Register<JsonSerializer>(() => new JsonSerializer());
            bucket.Register<StringSerializer>(() => new StringSerializer());
        }
    }
}
