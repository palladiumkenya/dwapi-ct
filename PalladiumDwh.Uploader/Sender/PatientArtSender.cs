using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using PalladiumDwh.Uploader.Controller;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Sender
{
    public class PatientArtSender : ISender<PatientArtExtract>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientArtSender));
        private readonly HttpClient _client;
        private const string Url = "http://localhost:55702/api/PatientArt";

        private readonly PatientArtController _patientArtController;
        public PatientArtSender()
        {
            _client = new HttpClient { BaseAddress = new Uri(Url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _patientArtController = new PatientArtController();
        }
 
        public async Task PostEntity(PatientArtExtract patientArtExtract)
        {
            var responseMessage = await _client.PostAsJsonAsync(Url, patientArtExtract);
            if (responseMessage.IsSuccessStatusCode)
            {
                //Todo: use Logger to log message 
                Log.Info("Successfully Uploaded ART Patient with CCC Number:  " + patientArtExtract.PatientCccNumber);
            }
            else
            {
                //Todo: use logger to log message
                Log.Error("Unable to Upload Art Patient with id:  " + patientArtExtract.PatientCccNumber + "Error was:  " + responseMessage.Content);
            }
        }

        public async Task CreateEntity(DataGridView gridView, PatientArtExtract patientArtExtract, IProgress<decimal> progress)
        {
            foreach (DataGridViewRow dr in gridView.Rows)
            {
                patientArtExtract.Id = (int)dr.Cells["Id"].Value;
                patientArtExtract.PatientCccNumber = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientCccNumber"].Value)) ? null : dr.Cells["PatientCccNumber"].Value.ToString();
                patientArtExtract.AgeEnrollment = (Decimal?)dr.Cells["AgeEnrollment"].Value;
                patientArtExtract.AgeARTStart = (Decimal?)dr.Cells["AgeArtStart"].Value;
                patientArtExtract.AgeLastVisit = (Decimal?)dr.Cells["AgeLastVisit"].Value;
                patientArtExtract.SiteCode = (int)dr.Cells["SiteCode"].Value; 
                patientArtExtract.FacilityName = string.IsNullOrEmpty(Convert.ToString(dr.Cells["FacilityName"].Value)) ? null : dr.Cells["FacilityName"].Value.ToString();
                patientArtExtract.RegistrationDate = (DateTime?)dr.Cells["RegistrationDate"].Value;
                patientArtExtract.PatientSource = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientSource"].Value)) ? null : dr.Cells["PatientSource"].Value.ToString();
                patientArtExtract.Gender = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Gender"].Value)) ? null : dr.Cells["Gender"].Value.ToString();
                patientArtExtract.StartARTDate = (DateTime?)dr.Cells["StartARTDate"].Value;
                patientArtExtract.PreviousARTStartDate = (DateTime?)dr.Cells["PreviousARTStartDate"].Value;
                patientArtExtract.PreviousARTRegimen = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PreviousARTRegimen"].Value)) ? null : dr.Cells["PreviousARTRegimen"].Value.ToString();
                patientArtExtract.StartARTAtThisFacility = (DateTime?)dr.Cells["StartARTAtThisFacility"].Value;
                patientArtExtract.StartRegimen = string.IsNullOrEmpty(Convert.ToString(dr.Cells["StartRegimen"].Value)) ? null : dr.Cells["StartRegimen"].Value.ToString();
                patientArtExtract.StartRegimenLine = string.IsNullOrEmpty(Convert.ToString(dr.Cells["StartRegimenLine"].Value)) ? null : dr.Cells["StartRegimenLine"].Value.ToString();
                patientArtExtract.LastARTDate = (DateTime?)dr.Cells["LastARTDate"].Value;
                patientArtExtract.LastRegimen = string.IsNullOrEmpty(Convert.ToString(dr.Cells["LastRegimen"].Value)) ? null : dr.Cells["LastRegimen"].Value.ToString();
                patientArtExtract.Duration = (Decimal?)dr.Cells["Duration"].Value;
                patientArtExtract.ExpectedReturn = (DateTime?)dr.Cells["ExpectedReturn"].Value;
                patientArtExtract.LastVisit = (DateTime?)dr.Cells["LastVisit"].Value;
                patientArtExtract.ExitReason = string.IsNullOrEmpty(Convert.ToString(dr.Cells["ExitReason"].Value)) ? null : dr.Cells["ExitReason"].Value.ToString();
                patientArtExtract.ExitDate = (DateTime?)dr.Cells["ExitDate"].Value;
                patientArtExtract.Emr = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Emr"].Value)) ? null : dr.Cells["Emr"].Value.ToString();
                patientArtExtract.Project = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Project"].Value)) ? null : dr.Cells["Project"].Value.ToString();
                patientArtExtract.PatientId = (int)dr.Cells["PatientId"].Value;
            
                await PostEntity(patientArtExtract);
                UpdateUploadedArtPatient(patientArtExtract.Id, patientArtExtract);
                if (progress != null)
                    progress.Report(dr.Index);
            }
        }
        private void UpdateUploadedArtPatient(int id, PatientArtExtract entity)
        {
            _patientArtController.UpdateUploadedPatientArt(id,entity);
        }
    }
}
