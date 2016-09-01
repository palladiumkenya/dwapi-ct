using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using PalladiumDwh.Uploader.Controller;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Sender
{
    public class PatientBaselineSender : ISender<PatientBaselinesExtract>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientBaselineSender));
        private readonly HttpClient _client;
        private const string Url = "http://localhost:55702/api/PatientBaseLine";

        private PatientBaselinesController _patientBaselinesController;

        public PatientBaselineSender()
        {
             _client = new HttpClient { BaseAddress = new Uri(Url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _patientBaselinesController = new PatientBaselinesController();
        }
        public async Task PostEntity(PatientBaselinesExtract patientBaselinesExtract)
        {
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync(Url, patientBaselinesExtract);
            if (responseMessage.IsSuccessStatusCode)
            {
                //Todo: use Logger to log message 
                Log.Info("Successfully Uploaded Patient Baseline Record with id with CCC Number :   " + patientBaselinesExtract.PatientCccNumber);
            }
            else
            {
                //Todo: use logger to log message
                Log.Error("Unable to Upload Patient Baseline with id  : " + patientBaselinesExtract.PatientCccNumber + " Error was: " + responseMessage.Content);
            }
        }

        public async Task CreateEntity(DataGridView gridView, PatientBaselinesExtract patientBaselinesExtract, IProgress<decimal> progress)
        {
            foreach (DataGridViewRow dr in gridView.Rows)
            {
                patientBaselinesExtract.PatientCccNumber = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientCccNumber"].Value)) ? null : dr.Cells["PatientCccNumber"].Value.ToString();
                patientBaselinesExtract.SiteCode = (int)dr.Cells["SiteCode"].Value;
                patientBaselinesExtract.eCD4 = (int?)dr.Cells["eCd4"].Value;
                patientBaselinesExtract.eCD4Date = (DateTime?)dr.Cells["eCD4Date"].Value;
                patientBaselinesExtract.eWHO = (int?)dr.Cells["eWHO"].Value;
                patientBaselinesExtract.eWHODate = (DateTime?)dr.Cells["eWHODate"].Value;
                patientBaselinesExtract.bCD4 = (int?)dr.Cells["bCD4"].Value;
                patientBaselinesExtract.eCD4Date = (DateTime?)dr.Cells["eCD4Date"].Value;
                patientBaselinesExtract.bWHO = (int?)dr.Cells["bWHO"].Value;
                patientBaselinesExtract.bWHODate = (DateTime?)dr.Cells["bWHODate"].Value;
                patientBaselinesExtract.lastCD4 = (int?)dr.Cells["lastCD4"].Value;
                patientBaselinesExtract.lastCD4Date = (DateTime?)dr.Cells["lastCD4Date"].Value;
                patientBaselinesExtract.m12CD4 = (int?)dr.Cells["m12CD4"].Value;
                patientBaselinesExtract.m12CD4Date = (DateTime?)dr.Cells["m12CD4Date"].Value;
                patientBaselinesExtract.m6CD4 = (int?)dr.Cells["m6CD4"].Value;
                patientBaselinesExtract.m6CD4Date = (DateTime?)dr.Cells["m6CD4Date"].Value;
                patientBaselinesExtract.Emr = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Emr"].Value)) ? null : dr.Cells["Emr"].Value.ToString();
                patientBaselinesExtract.Project = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Project"].Value)) ? null : dr.Cells["Project"].Value.ToString();
                patientBaselinesExtract.PatientId = (int)dr.Cells["PatientId"].Value;
               UpdateUploadedBaselinePatient(patientBaselinesExtract.Id,  patientBaselinesExtract);
                await PostEntity(patientBaselinesExtract);
                if (progress != null)
                    progress.Report(dr.Index);
            }
        }

        private void UpdateUploadedBaselinePatient(int id,PatientBaselinesExtract entity)
        {
            _patientBaselinesController.UpdateUploadedPatientBaseline(id, entity);
        }
    }
}
