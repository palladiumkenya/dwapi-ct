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
   

    public  class PatientSender : ISender<PatientExtract>
    {
        //Todo: Put this to base class
        private static readonly ILog Log = LogManager.GetLogger(typeof (PatientSender));
        private readonly HttpClient _client;
        private string url = "http://localhost:55702/api/patient";
        private readonly PatientExtractController _patientExtractController;
        public PatientSender()
        {
            _client = new HttpClient { BaseAddress = new Uri(url) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _patientExtractController = new PatientExtractController();
        }
        public async Task PostEntity(PatientExtract patientExtract)
        {
            var responseMessage = await _client.PostAsJsonAsync(url, patientExtract);
            if (responseMessage.IsSuccessStatusCode)
            {
                //Todo: use Logger to log message 
                Log.Info("Successfully Uploaded Patient with CCC Number: " + patientExtract.PatientCccNumber);
            }
            else
            {
                //Todo: use logger to log message
                Log.Error("Error Occured when Uploading Patient:  " + responseMessage.Content);
            }
        }
        public async  Task CreateEntity(DataGridView gridView, PatientExtract patientExtract, IProgress<decimal> progress)
        {
            foreach (DataGridViewRow dr in gridView.Rows)
            {
                patientExtract.Id = (int)dr.Cells["Id"].Value;
                patientExtract.PatientCccNumber = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientCccNumber"].Value)) ? null : dr.Cells["PatientCccNumber"].Value.ToString();
                patientExtract.SiteCode = (int)dr.Cells["SiteCode"].Value;
                patientExtract.FacilityName = string.IsNullOrEmpty(Convert.ToString(dr.Cells["FacilityName"].Value)) ? null : dr.Cells["FacilityName"].Value.ToString();
                patientExtract.Gender = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Gender"].Value)) ? null : dr.Cells["Gender"].Value.ToString();
                patientExtract.DOB = (DateTime?)dr.Cells["DOB"].Value;
                patientExtract.RegistrationDate = (DateTime?)dr.Cells["RegistrationDate"].Value;
                patientExtract.RegistrationAtCCC = (DateTime?)dr.Cells["RegistrationAtCCC"].Value;
                patientExtract.RegistrationATPMTCT = (DateTime?)dr.Cells["RegistrationATPMTCT"].Value;
                patientExtract.RegistrationAtTBClinic = (DateTime?)dr.Cells["RegistrationAtTBClinic"].Value;
                patientExtract.PatientSource = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientSource"].Value)) ? null : dr.Cells["PatientSource"].Value.ToString();
                patientExtract.Region = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Region"].Value)) ? null : dr.Cells["Region"].Value.ToString();
                patientExtract.District = string.IsNullOrEmpty(Convert.ToString(dr.Cells["District"].Value)) ? null : dr.Cells["District"].Value.ToString();
                patientExtract.Village = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Village"].Value)) ? null : dr.Cells["Village"].Value.ToString();
                patientExtract.ContactRelation = string.IsNullOrEmpty(Convert.ToString(dr.Cells["ContactRelation"].Value)) ? null : dr.Cells["ContactRelation"].Value.ToString();
                patientExtract.LastVisit = (DateTime?)dr.Cells["LastVisit"].Value;
                patientExtract.MaritalStatus = string.IsNullOrEmpty(Convert.ToString(dr.Cells["MaritalStatus"].Value)) ? null : dr.Cells["MaritalStatus"].Value.ToString();
                patientExtract.EducationLevel = string.IsNullOrEmpty(Convert.ToString(dr.Cells["EducationLevel"].Value)) ? null : dr.Cells["EducationLevel"].Value.ToString();
                patientExtract.DateConfirmedHIVPositive = (DateTime?)dr.Cells["DateConfirmedHIVPositive"].Value;
                patientExtract.PreviousARTExposure = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PreviousARTExposure"].Value)) ? null : dr.Cells["PreviousARTExposure"].Value.ToString();
                patientExtract.PreviousARTStartDate = (DateTime?)dr.Cells["PreviousARTStartDate"].Value;
                patientExtract.Emr = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Emr"].Value)) ? null : dr.Cells["Emr"].Value.ToString();
                patientExtract.Project = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Project"].Value)) ? null : dr.Cells["Project"].Value.ToString();
                await PostEntity(patientExtract);
                UpdateUploadedPatient(patientExtract.Id, patientExtract.SiteCode, patientExtract);
                if (progress != null)
                    progress.Report(dr.Index);
            }
        }

        private void UpdateUploadedPatient(int id, int siteCode, PatientExtract entity)
        {
            _patientExtractController.UpdateUploadedPatient(id, siteCode,entity);
        }
    }
}
