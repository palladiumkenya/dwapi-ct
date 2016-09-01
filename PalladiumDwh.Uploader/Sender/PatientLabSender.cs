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
    public class PatientLabSender : ISender<PatientLaboratoryExtract>
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientLabSender));
        private readonly HttpClient _client;
        private const string Url = "http://localhost:55702/api/PatientLab";

        private readonly PatientLaboratoryController _patientLaboratoryController; 
        public PatientLabSender()
        {
            _client = new HttpClient { BaseAddress = new Uri(Url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _patientLaboratoryController = new PatientLaboratoryController();
        }
        public async Task PostEntity(PatientLaboratoryExtract patientLabExtract)
        {
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync(Url, patientLabExtract);
            if (responseMessage.IsSuccessStatusCode)
            {
                //Todo: use Logger to log message 
                Log.Info("Successfully Uploaded Patient Lab data with CCC Number: " + patientLabExtract.PatientCccNumber);
            }
            else
            {
                //Todo: use logger to log message
                Log.Error("Error Occured when Uploading Patient Lab data:  " + responseMessage.Content);
            }
        }

        public async Task CreateEntity(DataGridView gridView, 
            PatientLaboratoryExtract patientLabExtract, IProgress<decimal> progress)
        {
            foreach (DataGridViewRow dr in gridView.Rows)
            {
                patientLabExtract.Id = (int)dr.Cells["Id"].Value;
                patientLabExtract.PatientId = (int)dr.Cells["PatientId"].Value;
                patientLabExtract.PatientCccNumber = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientCccNumber"].Value)) ? null : dr.Cells["PatientCccNumber"].Value.ToString();
                patientLabExtract.SiteCode = (int)dr.Cells["SiteCode"].Value;
                patientLabExtract.VisitId = (int)dr.Cells["VisitId"].Value;
                patientLabExtract.OrderedByDate = (DateTime?)dr.Cells["OrderedByDate"].Value;
                patientLabExtract.ReportedByDate = (DateTime)dr.Cells["ReportedByDate"].Value;
                patientLabExtract.TestName = string.IsNullOrEmpty(Convert.ToString(dr.Cells["TestName"].Value)) ? null : dr.Cells["TestName"].Value.ToString();
                patientLabExtract.TestResult = string.IsNullOrEmpty(Convert.ToString(dr.Cells["TestResult"].Value)) ? null : dr.Cells["TestResult"].Value.ToString();
                patientLabExtract.Emr = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Emr"].Value)) ? null : dr.Cells["Emr"].Value.ToString();
                patientLabExtract.Project = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Project"].Value)) ? null : dr.Cells["Project"].Value.ToString();
               
                await PostEntity(patientLabExtract);
                UpdateUploadedLaboratoryPatient(patientLabExtract.Id, patientLabExtract);
                if (progress != null)
                    progress.Report(dr.Index);

            }
        }

        private void UpdateUploadedLaboratoryPatient(int id,  PatientLaboratoryExtract entity)
        {
            _patientLaboratoryController.UpdateUploadedPatientLaboratory(id,  entity);
        }
    }
}
