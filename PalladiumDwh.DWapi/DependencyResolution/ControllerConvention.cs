using System.Web.Mvc;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace PalladiumDwh.DWapi.DependencyResolution
{
    public class ControllerConvention : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, Registry registry)
        {
            foreach (var type in types.AllTypes())
                if (type.CanBeCastTo<Controller>() && !type.IsAbstract)
                    registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
        }
    }
}