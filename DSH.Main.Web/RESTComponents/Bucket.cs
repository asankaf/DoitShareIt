using System;
using System.Collections.Generic;

namespace DSH.Main.Web.RESTComponents
{
    public interface IBucketResource
    {
        bool IsReusable { get; }
    }
    
    public class Bucket
    {
        #region Singleton

        public static Bucket Instance
        {
            get;
            private set;
        }
        
        static Bucket()
        {
            if (Instance == null)
            {
                Instance = new Bucket();
            }
        }

        private Bucket() 
        {
            reusableResources = new Dictionary<Type, IBucketResource>();
            instanceResources = new Dictionary<Type, Func<IBucketResource>>();
        }

        #endregion

        #region Fields

        Dictionary<Type, IBucketResource> reusableResources;
        Dictionary<Type, Func<IBucketResource>> instanceResources;

        #endregion

        #region Public Methods

        public void Register<T>(Func<IBucketResource> constructor) where T : class, IBucketResource
        {
            var item = constructor();
            
            if (item.IsReusable)
            {
                reusableResources.Add(typeof(T), item);
            }
            else
            {
                instanceResources.Add(typeof(T), constructor);
            }
        }

        public T Resolve<T>() where T : class, IBucketResource
        {
            try
            {
                var item = Fetch<T>();
                return item as T;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Private Methods

        IBucketResource Fetch<T>()
        { 
            IBucketResource item = null;
            if (reusableResources.TryGetValue(typeof(T), out item))
            {
                return item;
            }

            Func<IBucketResource> constructor = null;
            if (instanceResources.TryGetValue(typeof(T), out constructor))
            {
                return constructor();
            }

            throw Errors.ResourceNotFound(typeof(T).Name);
        }

        #endregion
    }
}
