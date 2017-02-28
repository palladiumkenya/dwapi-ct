using System.Collections.Generic;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
    public class DataSetController
    {
        private readonly IDataSetRepository _repository = null;
        public DataSetController()
        {
            _repository = new DataSetRepository(new DwhClientEntities());
        }

        public void SaveDataSet(DataSet dataSet)
        {
            _repository.Post(dataSet);
        }
        public IEnumerable<DataSet> GetLoadedDataSets()
        {
            return _repository.Get();
        }
    }
} 
