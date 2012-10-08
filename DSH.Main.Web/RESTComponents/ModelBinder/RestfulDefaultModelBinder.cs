using System;
using System.Linq;
using System.Web.Mvc;

namespace DSH.Main.Web.RESTComponents.ModelBinder
{
    public class RestfulDefaultModelBinder : DefaultModelBinder
    {
        static readonly Type[] notSupported = new Type[] 
        { 
            typeof(int),
            typeof(string)
        };

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type modelType = bindingContext.ModelType;

            if (modelType.IsSerializable && !notSupported.Contains(modelType))
            {
                var binder = RestfulModelBinderFactory.GetBinder(
                    controllerContext.HttpContext.Request.ContentType
                        .ToLower());

                if (binder != null)
                {
                    return binder.Deserialize(
                        controllerContext.HttpContext.Request.InputStream,
                        modelType);
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
