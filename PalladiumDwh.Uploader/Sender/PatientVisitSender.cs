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
    public class PatientVisitSender : ISender<PatientVisitExtract>
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientVisitSender));
        private readonly HttpClient _client;
        private string url = "http://localhost:55702/api/PatientVisit";

        private readonly PatientVisitController _patientVisitController;

        public PatientVisitSender()
        {
            _client = new HttpClient {BaseAddress = new Uri(url)};
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _patientVisitController = new PatientVisitController();

        }

        public async Task PostEntity(PatientVisitExtract patientVisitExtract)
        {
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync(url, patientVisitExtract);
            if (responseMessage.IsSuccessStatusCode)
            {
                //Todo: use Logger to log message 
                Log.Info("Successfully Uploaded Patient Visits data with CCC Number: " +
                         patientVisitExtract.PatientCccNumber);
            }
            else
            {
                //Todo: use logger to log message
                Log.Error("Error Occured when Uploading Visits:  " + responseMessage.Content);
            }
        }

        public async Task CreateEntity(DataGridView gridView, PatientVisitExtract patientVisitExtract,
            IProgress<decimal> progress)
        {
            foreach (DataGridViewRow dr in gridView.Rows)
            {
                patientVisitExtract.Id = (int) dr.Cells["Id"].Value;
                patientVisitExtract.PatientId = (int) dr.Cells["PatientId"].Value;
                patientVisitExtract.PatientCccNumber =
                    string.IsNullOrEmpty(Convert.ToString(dr.Cells["PatientCccNumber"].Value))
                        ? null
                        : dr.Cells["PatientCccNumber"].Value.ToString();
                patientVisitExtract.SiteCode = (int)dr.Cells["SiteCode"].Value;
                patientVisitExtract.VisitID = (int?) dr.Cells["VisitID"].Value;
                patientVisitExtract.VisitDate = (DateTime?) dr.Cells["VisitDate"].Value;
                patientVisitExtract.Service = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Service"].Value))
                    ? null
                    : dr.Cells["Service"].Value.ToString();
                patientVisitExtract.VisitType = string.IsNullOrEmpty(Convert.ToString(dr.Cells["VisitType"].Value))
                    ? null
                    : dr.Cells["VisitType"].Value.ToString();
                patientVisitExtract.WHOStage = (int?) dr.Cells["WHOStage"].Value;
                patientVisitExtract.WABStage = string.IsNullOrEmpty(Convert.ToString(dr.Cells["WABStage"].Value))
                    ? null
                    : dr.Cells["WABStage"].Value.ToString();
                patientVisitExtract.Pregnant = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Pregnant"].Value))
                    ? null
                    : dr.Cells["Pregnant"].Value.ToString();
                patientVisitExtract.LMP = (DateTime?) dr.Cells["LMP"].Value;
                patientVisitExtract.EDD = (DateTime?) dr.Cells["EDD"].Value;
                patientVisitExtract.Height = (Decimal?) dr.Cells["Height"].Value;
                patientVisitExtract.Weight = (Decimal?) dr.Cells["Weight"].Value;
                patientVisitExtract.BP = string.IsNullOrEmpty(Convert.ToString(dr.Cells["BP"].Value))
                    ? null
                    : dr.Cells["BP"].Value.ToString();
                patientVisitExtract.OI = string.IsNullOrEmpty(Convert.ToString(dr.Cells["OI"].Value))
                    ? null
                    : dr.Cells["OI"].Value.ToString();
                patientVisitExtract.OIDate = (DateTime?) dr.Cells["OIDate"].Value;
                patientVisitExtract.SubstitutionFirstlineRegimenDate =
                    (DateTime?) dr.Cells["SubstitutionFirstlineRegimenDate"].Value;
                patientVisitExtract.SubstitutionFirstlineRegimenReason =
                    string.IsNullOrEmpty(Convert.ToString(dr.Cells["SubstitutionFirstlineRegimenReason"].Value))
                        ? null
                        : dr.Cells["SubstitutionFirstlineRegimenReason"].Value.ToString();
                patientVisitExtract.SubstitutionSecondlineRegimenDate =
                    (DateTime?) dr.Cells["SubstitutionSecondlineRegimenDate"].Value;
                patientVisitExtract.SubstitutionSecondlineRegimenReason =
                    string.IsNullOrEmpty(Convert.ToString(dr.Cells["SubstitutionSecondlineRegimenReason"].Value))
                        ? null
                        : dr.Cells["SubstitutionSecondlineRegimenReason"].Value.ToString();
                patientVisitExtract.SecondlineRegimenChangeDate =
                    (DateTime?) dr.Cells["SecondlineRegimenChangeDate"].Value;
                patientVisitExtract.SecondlineRegimenChangeReason =
                    string.IsNullOrEmpty(Convert.ToString(dr.Cells["SecondlineRegimenChangeReason"].Value))
                        ? null
                        : dr.Cells["SubstitutionFirstlineRegimenReason"].Value.ToString();
                patientVisitExtract.Adherence = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Adherence"].Value))
                    ? null
                    : dr.Cells["Adherence"].Value.ToString();
                patientVisitExtract.AdherenceCategory =
                    string.IsNullOrEmpty(Convert.ToString(dr.Cells["AdherenceCategory"].Value))
                        ? null
                        : dr.Cells["AdherenceCategory"].Value.ToString();
                patientVisitExtract.FamilyPlanningMethod =
                    string.IsNullOrEmpty(Convert.ToString(dr.Cells["FamilyPlanningMethod"].Value))
                        ? null
                        : dr.Cells["FamilyPlanningMethod"].Value.ToString();
                patientVisitExtract.PwP = string.IsNullOrEmpty(Convert.ToString(dr.Cells["PwP"].Value))
                    ? null
                    : dr.Cells["PwP"].Value.ToString();
                patientVisitExtract.GestationAge = (Decimal?) dr.Cells["GestationAge"].Value;
                patientVisitExtract.NextAppointmentDate = (DateTime?) dr.Cells["NextAppointmentDate"].Value;
                patientVisitExtract.Emr = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Emr"].Value))
                    ? null
                    : dr.Cells["Emr"].Value.ToString();
                patientVisitExtract.Project = string.IsNullOrEmpty(Convert.ToString(dr.Cells["Project"].Value))
                    ? null
                    : dr.Cells["Project"].Value.ToString();
              
                await PostEntity(patientVisitExtract);
                UpdateUploadedPatientVisits(patientVisitExtract.Id, patientVisitExtract);
                if (progress != null)
                    progress.Report(dr.Index);
            }
        }

        private void UpdateUploadedPatientVisits(int id,  PatientVisitExtract entity)
        {
            _patientVisitController.UpdateUploadedPatientVisit(id,  entity);
        }
    }
}
