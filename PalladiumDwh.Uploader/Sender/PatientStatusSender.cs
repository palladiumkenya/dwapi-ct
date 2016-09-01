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
    public class PatientStatusSender : ISender<PatientStatusExtract>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientStatusSender));
        private readonly HttpClient _client;
        private string url = "http://localhost:55702/api/PatientStatus";
        private readonly PatientStatusController _patientStatusController;
      
        public PatientStatusSender()
        {
            _client = new HttpClient { BaseAddress = new Uri(url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _patientStatusController = new PatientStatusController();
        }

        public async Task PostEntity(PatientStatusExtract patientStatusExtract)
        {
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync(url, patientStatusExtract);
            if (responseMessage.IsSuccessStatusCode)
            {
                //Todo: use Logger to log message 
                Log.Info("Successfully Uploaded Patient Status with CCC Number: " + patientStatusExtract.PatientCccNumber);
            }
            else
            {
                //Todo: use logger to log message
                Log.Error("Error Occured when Uploading Patient Status:  " + responseMessage.Content);
            }
        }

        public async Task CreateEntity(DataGridView gridView, PatientStatusExtract patientStatusExtract, IProgress<decimal> progress)
        {
            foreach (DataGridViewRow dr in gridView.Rows)
            {
                patientStatusExtract.Id = (int)dr.Cells["Id"].Value;
                patientStatusExtract.PatientId = (int) dr.Cells["PatientId"].Value;
                patientStatusExtract.PatientCccNumber = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientCccNumber"].Value)) ? null : dr.Cells["PatientCccNumber"].Value.ToString();
                patientStatusExtract.SiteCode = (int)dr.Cells["SiteCode"].Value;
                patientStatusExtract.FacilityName = string.IsNullOrEmpty(Convert.ToString(dr.Cells["FacilityName"].Value)) ? null : dr.Cells["FacilityName"].Value.ToString();
                patientStatusExtract.ExitDescription = string.IsNullOrEmpty(Convert.ToString(dr.Cells["ExitDescription"].Value)) ? null : dr.Cells["ExitDescription"].Value.ToString();
                patientStatusExtract.ExitDate = (DateTime?)dr.Cells["ExitDate"].Value;
                patientStatusExtract.ExitReason = string.IsNullOrEmpty(Convert.ToString(dr.Cells["ExitReason"].Value)) ? null : dr.Cells["ExitReason"].Value.ToString();
                patientStatusExtract.Emr = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Emr"].Value)) ? null : dr.Cells["Emr"].Value.ToString();
                patientStatusExtract.Project = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Project"].Value)) ? null : dr.Cells["Project"].Value.ToString();
               
                await PostEntity(patientStatusExtract);
                UpdateUploadedPatientStatus(patientStatusExtract.Id, patientStatusExtract);
                if (progress != null)
                    progress.Report(dr.Index);
            }
        }


        private void UpdateUploadedPatientStatus(int id,  PatientStatusExtract entity)
        {
            _patientStatusController.UpdateUploadedPatientStatus(id, entity);
        }
    }
}
