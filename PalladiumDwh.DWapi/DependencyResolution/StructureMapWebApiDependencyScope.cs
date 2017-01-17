using System.Web.Http.Dependencies;
using StructureMap;

namespace PalladiumDwh.DWapi.DependencyResolution
{
    /// <summary>
    ///     The structure map web api dependency scope.
    /// </summary>
    public class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
    {
        public StructureMapWebApiDependencyScope(IContainer container)
            : base(container)
        {
        }
    }
}