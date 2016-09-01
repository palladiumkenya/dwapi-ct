using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Repository;

namespace PalladiumDwh.Uploader.Controller
{
   public  class ManagerController
    {
        /// <summary>
        /// The _repository
        /// </summary>
     
       private const string PatientDataFile = "Patient";
       private const string PatientArtDataFile = "Patient ART";
       private const string PatientPharmacyDataFile = "Patient Pharmacy";
       private const string PatientLabDataFile = "Patient Laboratory";
       private const string PatientVisitDataFile = "Patient Visit";
       private const string PatientBaselineFile = "Patient Baselines";
       private const string PatientStatusFile = "Patient Status";

       private readonly string[] _dataSetFiles =
       {
           PatientDataFile, PatientArtDataFile, PatientPharmacyDataFile,
           PatientLabDataFile, PatientVisitDataFile, PatientBaselineFile,
           PatientStatusFile
       };

       private readonly PatientExtractController _patientExtractController;
       private readonly PatientArtController _patitnetArtController;
       private readonly PatientBaselinesController _patientBaselinesController;
       private readonly PatientLaboratoryController _patientLaboratoryController;
       private readonly PatientPharmacyController _patientPharmacyController;
       private readonly PatientStatusController _patientStatusController;
       private readonly PatientVisitController _patientVisitController;
       private readonly FacilityController _facilityController;
       private readonly DataSetController _dataSetController;

         public ManagerController()
        {
            _patientExtractController = new PatientExtractController();
            _patitnetArtController = new PatientArtController();
            _patientBaselinesController = new PatientBaselinesController();
            _patientLaboratoryController = new PatientLaboratoryController();
            _patientPharmacyController = new PatientPharmacyController();
            _patientStatusController = new PatientStatusController();
            _patientVisitController = new PatientVisitController();
            _facilityController = new FacilityController();
             _dataSetController = new DataSetController();
          
        }
         /// <summary>
         /// Saves the data set.
         /// </summary>
         /// <param name="dataSet">The data set.</param>
       public void SaveDataSet(DataSet dataSet)
       {
           if (_dataSetController.GetLoadedDataSets() != null) return;
           foreach (var dataSetFile in _dataSetFiles)
           {
               dataSet.DataSetName = dataSetFile;
               _dataSetController.SaveDataSet(dataSet);
           }
       }

       /// <summary>
       /// Loads the patients from iq tools.
       /// </summary>
       /// <returns></returns>
       public int LoadPatientsFromIqTools(string iqToolsdb, string iqToolsServer)
       {
           return _patientExtractController.LoadFromIqTools(iqToolsdb, iqToolsServer);
       }
       /// <summary>
       /// Loads the art patients from iq tools.
       /// </summary>
       /// <param name="iqToolsdb">The iq toolsdb.</param>
       /// <param name="iqToolsServer">The iq tools server.</param>
       /// <returns></returns>
       public int LoadArtPatientsFromIqTools(string iqToolsdb, string iqToolsServer)
       {
           return _patitnetArtController.LoadFromIqTools(iqToolsdb, iqToolsServer);
       }
       /// <summary>
       /// Loads the baseline patients from iq tools.
       /// </summary>
       /// <param name="iqToolsdb">The iq toolsdb.</param>
       /// <param name="iqToolsServer">The iq tools server.</param>
       /// <returns></returns>
       public int LoadBaselinePatientsFromIqTools(string iqToolsdb, string iqToolsServer)
       {
           return _patientBaselinesController.LoadFromIqTools(iqToolsdb, iqToolsServer);
       }
       /// <summary>
       /// Loads the laboratory patients from iq tools.
       /// </summary>
       /// <param name="iqToolsdb">The iq toolsdb.</param>
       /// <param name="iqToolsServer">The iq tools server.</param>
       /// <returns></returns>
       public  int LoadLaboratoryPatientsFromIqTools(string iqToolsdb, string iqToolsServer)
       {
           return _patientLaboratoryController.LoadFromIqTools(iqToolsdb, iqToolsServer);
       }
       /// <summary>
       /// Loads the pharmacy patients from iq tools.
       /// </summary>
       /// <param name="iqToolsdb">The iq toolsdb.</param>
       /// <param name="iqToolsServer">The iq tools server.</param>
       /// <returns></returns>
       public int LoadPharmacyPatientsFromIqTools(string iqToolsdb, string iqToolsServer)
       {
           return _patientPharmacyController.LoadFromIqTools(iqToolsdb, iqToolsServer);
       }
       /// <summary>
       /// Loads the s patient status from iq tools.
       /// </summary>
       /// <param name="iqToolsdb">The iq toolsdb.</param>
       /// <param name="iqToolsServer">The iq tools server.</param>
       /// <returns></returns>
       public int LoadPatientStatusFromIqTools(string iqToolsdb, string iqToolsServer)
       {
           return _patientStatusController.LoadFromIqTools(iqToolsdb, iqToolsServer);
       }
       /// <summary>
       /// Loads the patient visits from iq tools.
       /// </summary>
       /// <param name="iqToolsdb">The iq toolsdb.</param>
       /// <param name="iqToolsServer">The iq tools server.</param>
       /// <returns></returns>
       public int LoadPatientVisitsFromIqTools(string iqToolsdb, string iqToolsServer)
       {
           return _patientVisitController.LoadFromIqTools(iqToolsdb, iqToolsServer);
       }
       /// <summary>
       /// Populates the grid view.
       /// </summary>
       /// <param name="gridViews">The grid views.</param>
       public void PopulateGridView(DataGridView[] gridViews)
         {
             PopulatePatientGridView(gridViews[0]);
             PopulatePatientArtGridView(gridViews[1]);
             PopulatePatientPatientPharmacyGridView(gridViews[2]);
             PopulatePatientLaboratoryGridView(gridViews[3]);
             PopulatePatientStatusGridView(gridViews[4]);
             PopulatePatientBaselineGridView(gridViews[5]);
             PopulatePatientVisitGridView(gridViews[6]);
         }


       /// <summary>
       /// Populates the TreeView.
       /// </summary>
       /// <param name="treeView">The tree view.</param>
       public void PopulateTreeView(TreeView treeView)
       {
           foreach (var f in _facilityController.GetLoadedFacilites().ToList())
           {
               var node = new TreeNode(f.FacilityName);

               foreach (var d in _dataSetController.GetLoadedDataSets().ToList())
               {
                   node.Nodes.Add(d.DataSetName);
               }
               treeView.Nodes.Add(node);
           }
       }
       /// <summary>
       /// Populates the patient grid view.
       /// </summary>
       /// <param name="gridView">The grid view.</param>
       private  void PopulatePatientGridView(DataGridView gridView)
       {
         gridView.DataSource = _patientExtractController.GetAllPatientExtracts();
           
       }
       /// <summary>
       /// Populates the patient art grid view.
       /// </summary>
       /// <param name="gridView">The grid view.</param>
       private  void PopulatePatientArtGridView(DataGridView gridView)
       {
          gridView.DataSource = _patitnetArtController.GetAllArtPatientExtracts();
           
       }
       /// <summary>
       /// Populates the patient patient pharmacy grid view.
       /// </summary>
       /// <param name="gridView">The grid view.</param>
       private void PopulatePatientPatientPharmacyGridView(DataGridView gridView)
       {

         gridView.DataSource = _patientPharmacyController.GetAllPatientPharmacyExtracts();

       }
       /// <summary>
       /// Populates the patient laboratory grid view.
       /// </summary>
       /// <param name="gridView">The grid view.</param>
       private  void PopulatePatientLaboratoryGridView(DataGridView gridView)
       {
            gridView.DataSource = _patientLaboratoryController.GetAllPatientLaboratoryExtracts();
       }
       /// <summary>
       /// Populates the patient status grid view.
       /// </summary>
       /// <param name="gridView">The grid view.</param>
       private void PopulatePatientStatusGridView(DataGridView gridView)
       {
           gridView.DataSource = _patientStatusController.GetAllPatientStatusExtracts();
           
       }
       /// <summary>
       /// Populates the patient baseline grid view.
       /// </summary>
       /// <param name="gridView">The grid view.</param>
       private  void PopulatePatientBaselineGridView(DataGridView gridView)
       {
           gridView.DataSource = _patientBaselinesController.GetAllBaselinePatientExtracts();

       }
       /// <summary>
       /// Populates the patient visit grid view.
       /// </summary>
       /// <param name="gridView">The grid view.</param>
       private  void PopulatePatientVisitGridView(DataGridView gridView)
       {
           gridView.DataSource = _patientVisitController.GetAllPatientVisitExtracts();
       }
    }
}
