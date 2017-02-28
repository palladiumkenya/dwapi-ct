using System.Collections.Generic;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class FacilityController
    {
        private readonly IFacilityRepository _repository = null;


        public FacilityController()
        {
            _repository = new FacilityRepository(new DwhClientEntities());
        }

        /// <summary>
        /// Gets the loaded facilites.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Facility> GetLoadedFacilites()
        {
            return _repository.Get();
        }

        public void SaveFacility(Facility facility)
        {
            _repository.Post(facility);
        }

    }
}
