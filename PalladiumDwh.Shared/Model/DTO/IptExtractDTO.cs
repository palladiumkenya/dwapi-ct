using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class IptExtractDTO : IIptExtractDTO
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string OnTBDrugs { get; set; }
        public string OnIPT { get; set; }
        public string EverOnIPT { get; set; }
        public string Cough { get; set; }
        public string Fever { get; set; }
        public string NoticeableWeightLoss { get; set; }
        public string NightSweats { get; set; }
        public string Lethargy { get; set; }
        public string ICFActionTaken { get; set; }
        public string TestResult { get; set; }
        public string TBClinicalDiagnosis { get; set; }
        public string ContactsInvited { get; set; }
        public string EvaluatedForIPT { get; set; }
        public string StartAntiTBs { get; set; }
        public DateTime? TBRxStartDate { get; set; }
        public string TBScreening { get; set; }
        public string IPTClientWorkUp { get; set; }
        public string StartIPT { get; set; }
        public string IndicationForIPT { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }
        public DateTime? TPTInitiationDate { get; set; }
        public string IPTDiscontinuation { get; set; }
        public DateTime? DateOfDiscontinuation { get; set; }
        public string Hepatoxicity { get; set; }
        public string PeripheralNeuropathy { get; set; }
        public string Rash { get; set; }
        public string Adherence { get; set; }

        public IptExtractDTO()
        {
        }

        public IptExtractDTO(IptExtract IptExtract)
        {
            FacilityName=IptExtract.FacilityName;
            VisitID=IptExtract.VisitID;
            VisitDate=IptExtract.VisitDate;
            OnTBDrugs=IptExtract.OnTBDrugs;
            OnIPT=IptExtract.OnIPT;
            EverOnIPT=IptExtract.EverOnIPT;
            Cough=IptExtract.Cough;
            Fever=IptExtract.Fever;
            NoticeableWeightLoss=IptExtract.NoticeableWeightLoss;
            NightSweats=IptExtract.NightSweats;
            Lethargy=IptExtract.Lethargy;
            ICFActionTaken=IptExtract.ICFActionTaken;
            TestResult=IptExtract.TestResult;
            TBClinicalDiagnosis=IptExtract.TBClinicalDiagnosis;
            ContactsInvited=IptExtract.ContactsInvited;
            EvaluatedForIPT=IptExtract.EvaluatedForIPT;
            StartAntiTBs=IptExtract.StartAntiTBs;
            TBRxStartDate=IptExtract.TBRxStartDate;
            TBScreening=IptExtract.TBScreening;
            IPTClientWorkUp=IptExtract.IPTClientWorkUp;
            StartIPT=IptExtract.StartIPT;
            IndicationForIPT=IptExtract.IndicationForIPT;
            TPTInitiationDate=IptExtract.TPTInitiationDate;
            IPTDiscontinuation=IptExtract.IPTDiscontinuation;
            DateOfDiscontinuation=IptExtract.DateOfDiscontinuation;
            Hepatoxicity= IptExtract.Hepatoxicity;
            PeripheralNeuropathy= IptExtract.PeripheralNeuropathy;
            Rash= IptExtract.Rash;
            Adherence = IptExtract.Adherence;

            Emr = IptExtract.Emr;
            Project = IptExtract.Project;
            PatientId = IptExtract.PatientId;
            Date_Created=IptExtract.Date_Created;
            Date_Last_Modified=IptExtract.Date_Last_Modified;
            RecordUUID=IptExtract.RecordUUID;
            Voided=IptExtract.Voided;

        }



        public IEnumerable<IptExtractDTO> GenerateIptExtractDtOs(IEnumerable<IptExtract> extracts)
        {
            var statusExtractDtos = new List<IptExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new IptExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public IptExtract GenerateIptExtract(Guid patientId)
        {
            PatientId = patientId;
            return new IptExtract(
                FacilityName,
                VisitID,
                VisitDate,
                OnTBDrugs,
                OnIPT,
                EverOnIPT,
                Cough,
                Fever,
                NoticeableWeightLoss,
                NightSweats,
                Lethargy,
                ICFActionTaken,
                TestResult,
                TBClinicalDiagnosis,
                ContactsInvited,
                EvaluatedForIPT,
                StartAntiTBs,
                TBRxStartDate,
                TBScreening,
                IPTClientWorkUp,
                StartIPT,
                IndicationForIPT,
                PatientId,
                Emr,Project,
                Date_Created,
                Date_Last_Modified,
                RecordUUID,
                Voided,
                TPTInitiationDate,
                IPTDiscontinuation,
                DateOfDiscontinuation,
                Hepatoxicity,
                PeripheralNeuropathy,
                Rash,
                Adherence 
                );
        }



    }
}
