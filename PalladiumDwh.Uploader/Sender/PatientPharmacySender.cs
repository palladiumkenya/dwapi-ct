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
    public class PatientPharmacySender : ISender<PatientPharmacyExtract>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientPharmacySender));
        private readonly HttpClient _client;
        private string url = "http://localhost:55702/api/PatientPharmacy";

        private readonly PatientPharmacyController _patientPharmacyController;
        public PatientPharmacySender()
        {
            _client = new HttpClient { BaseAddress = new Uri(url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _patientPharmacyController = new PatientPharmacyController();
  
        }

        public async Task PostEntity(PatientPharmacyExtract pharmacyExtract)
        {
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync(url, pharmacyExtract);
            if (responseMessage.IsSuccessStatusCode)
            {
                //Todo: use Logger to log message 
                Log.Info("Successfully Uploaded Patient Pharmacy data with CCC Number: " + pharmacyExtract.PatientCccNumber);
            }
            else
            {
                //Todo: use logger to log message
                Log.Error("Error Occured when Uploading Patient Pharmacy data:  " + responseMessage.Content);
            }
        }

        public async Task CreateEntity(DataGridView gridView, PatientPharmacyExtract pharmacyExtract, IProgress<decimal> progress)
        {
            foreach (DataGridViewRow dr in gridView.Rows)
            {
                pharmacyExtract.Id = (int)dr.Cells["Id"].Value;
                pharmacyExtract.PatientId = (int)dr.Cells["PatientId"].Value;
                pharmacyExtract.PatientCccNumber = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientCccNumber"].Value)) ? null : dr.Cells["PatientCccNumber"].Value.ToString();
                pharmacyExtract.SiteCode = (int)dr.Cells["SiteCode"].Value;
                pharmacyExtract.VisitID = (int)dr.Cells["VisitID"].Value;
                pharmacyExtract.Drug = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Drug"].Value)) ? null : dr.Cells["Drug"].Value.ToString();
                pharmacyExtract.DispenseDate = (DateTime?)dr.Cells["DispenseDate"].Value;
                pharmacyExtract.Duration = (Decimal?)dr.Cells["Duration"].Value; ;
                pharmacyExtract.ExpectedReturn = string.IsNullOrEmpty(Convert.ToString(dr.Cells["ExpectedReturn"].Value)) ? null : dr.Cells["ExpectedReturn"].Value.ToString();
                pharmacyExtract.TreatmentType = string.IsNullOrEmpty(Convert.ToString(dr.Cells["TreatmentType"].Value)) ? null : dr.Cells["TreatmentType"].Value.ToString();
                pharmacyExtract.PeriodTaken = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PeriodTaken"].Value)) ? null : dr.Cells["PeriodTaken"].Value.ToString();
                pharmacyExtract.ProphylaxisType = string.IsNullOrEmpty(Convert.ToString(dr.Cells["ProphylaxisType"].Value)) ? null : dr.Cells["ProphylaxisType"].Value.ToString(); 
                pharmacyExtract.Emr = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Emr"].Value)) ? null : dr.Cells["Emr"].Value.ToString();
                pharmacyExtract.Project = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Project"].Value)) ? null : dr.Cells["Project"].Value.ToString();
             
                await PostEntity(pharmacyExtract);
                UpdateUploadedPharmacyPatient(pharmacyExtract.Id, pharmacyExtract);
                if (progress != null)
                    progress.Report(dr.Index);

            }
        }

        private void UpdateUploadedPharmacyPatient(int id, PatientPharmacyExtract entity)
        {
            _patientPharmacyController.UpdateUploadedPatientPharmacy(id,entity);
        }
    }
}
