using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace PalladiumDwh.DWapi.DependencyResolution
{
    /// <summary>
    ///     The structure map dependency scope.
    /// </summary>
    public class StructureMapDependencyScope : ServiceLocatorImplBase
    {
        #region Constants and Fields

        private const string NestedContainerKey = "Nested.Container.Key";

        #endregion

        #region Constructors and Destructors

        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            Container = container;
        }

        #endregion

        #region Properties

        private HttpContextBase HttpContext
        {
            get
            {
                var ctx = Container.TryGetInstance<HttpContextBase>();
                return ctx ?? (System.Web.HttpContext.Current != null ? new HttpContextWrapper(System.Web.HttpContext.Current) : null);
            }
        }

        #endregion

        #region Public Properties

        public IContainer Container { get; set; }

        public IContainer CurrentNestedContainer
        {
            get
            {
                if (HttpContext != null)
                    return (IContainer)HttpContext.Items[NestedContainerKey];
                return null;
            }
            set
            {
                HttpContext.Items[NestedContainerKey] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void CreateNestedContainer()
        {
            if (CurrentNestedContainer != null) return;
            CurrentNestedContainer = Container.GetNestedContainer();
        }

        public void Dispose()
        {
            DisposeNestedContainer();
            Container.Dispose();
        }

        public void DisposeNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                CurrentNestedContainer.Dispose();
                CurrentNestedContainer = null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return DoGetAllInstances(serviceType);
        }

        #endregion

        #region Methods

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (CurrentNestedContainer ?? Container).GetAllInstances(serviceType).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            var container = CurrentNestedContainer ?? Container;

            if (string.IsNullOrEmpty(key))
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);

            return container.GetInstance(serviceType, key);
        }

        #endregion
    }
}