using System.Web.Http.Dependencies;
using StructureMap;

namespace PalladiumDwh.DWapi.DependencyResolution
{
    /// <summary>
    ///     The structure map dependency resolver.
    /// </summary>
    public class StructureMapWebApiDependencyResolver : StructureMapWebApiDependencyScope, IDependencyResolver
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StructureMapWebApiDependencyResolver" /> class.
        /// </summary>
        /// <param name="container">
        ///     The container.
        /// </param>
        public StructureMapWebApiDependencyResolver(IContainer container)
            : base(container)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The begin scope.
        /// </summary>
        /// <returns>
        ///     The System.Web.Http.Dependencies.IDependencyScope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            var child = Container.GetNestedContainer();
            return new StructureMapWebApiDependencyResolver(child);
        }

        #endregion
    }
}