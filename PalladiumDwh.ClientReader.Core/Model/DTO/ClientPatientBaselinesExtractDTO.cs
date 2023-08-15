using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.DTOs;

namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public class ClientPatientBaselinesExtractDTO : IClientPatientBaselinesExtractDTO
    {
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
        public int FacilityId { get; set; }
        public int? bCD4 { get; set; }
        public DateTime? bCD4Date { get; set; }
        public int? bWAB { get; set; }
        public DateTime? bWABDate { get; set; }
        public int? bWHO { get; set; }
        public DateTime? bWHODate { get; set; }
        public int? eWAB { get; set; }
        public DateTime? eWABDate { get; set; }
        public int? eCD4 { get; set; }
        public DateTime? eCD4Date { get; set; }
        public int? eWHO { get; set; }
        public DateTime? eWHODate { get; set; }
        public int? lastWHO { get; set; }
        public DateTime? lastWHODate { get; set; }
        public int? lastCD4 { get; set; }
        public DateTime? lastCD4Date { get; set; }
        public int? lastWAB { get; set; }
        public DateTime? lastWABDate { get; set; }
        public int? m12CD4 { get; set; }
        public DateTime? m12CD4Date { get; set; }
        public int? m6CD4 { get; set; }
        public DateTime? m6CD4Date { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }

        

        public ClientPatientBaselinesExtractDTO()
        {
        }

        public ClientPatientBaselinesExtractDTO(int patientPid, string patientCccNumber, int facilityId, int? bCd4, DateTime? bCd4Date, int? bWab, DateTime? bWabDate, int? bWho, DateTime? bWhoDate, int? eWab, DateTime? eWabDate, int? eCd4, DateTime? eCd4Date, int? eWho, DateTime? eWhoDate, int? lastWho, DateTime? lastWhoDate, int? lastCd4, DateTime? lastCd4Date, int? lastWab, DateTime? lastWabDate, int? m12Cd4, DateTime? m12Cd4Date, int? m6Cd4, DateTime? m6Cd4Date, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified, string PatientUUID)
        {
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
            FacilityId = facilityId;
            bCD4 = bCd4;
            bCD4Date = bCd4Date;
            bWAB = bWab;
            bWABDate = bWabDate;
            bWHO = bWho;
            bWHODate = bWhoDate;
            eWAB = eWab;
            eWABDate = eWabDate;
            eCD4 = eCd4;
            eCD4Date = eCd4Date;
            eWHO = eWho;
            eWHODate = eWhoDate;
            lastWHO = lastWho;
            lastWHODate = lastWhoDate;
            lastCD4 = lastCd4;
            lastCD4Date = lastCd4Date;
            lastWAB = lastWab;
            lastWABDate = lastWabDate;
            m12CD4 = m12Cd4;
            m12CD4Date = m12Cd4Date;
            m6CD4 = m6Cd4;
            m6CD4Date = m6Cd4Date;
            Emr = emr;
            Project = project;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            PatientUUID = PatientUUID;
        }


        public ClientPatientBaselinesExtractDTO(ClientPatientBaselinesExtract extract)
        {
            PatientPID = extract.PatientPK; //TODO PatientPID = extract.PatientPK;
            PatientCccNumber = extract.PatientID; //TODO PatientCccNumber = extract.PatientID;
            FacilityId = extract.SiteCode; //TODO FacilityId = extract.SiteCode
            bCD4 = extract.bCD4;
            bCD4Date = extract.bCD4Date;
            bWAB = extract.bWAB;
            bWABDate = extract.bWABDate;
            bWHO = extract.bWHO;
            bWHODate = extract.bWHODate;
            eWAB = extract.eWAB;
            eWABDate = extract.eWABDate;
            eCD4 = extract.eCD4;
            eCD4Date = extract.eCD4Date;
            eWHO = extract.eWHO;
            eWHODate = extract.eWHODate;
            lastWHO = extract.lastWHO;
            lastWHODate = extract.lastWHODate;
            lastCD4 = extract.lastCD4;
            lastCD4Date = extract.lastCD4Date;
            lastWAB = extract.lastWAB;
            lastWABDate = extract.lastWABDate;
            m12CD4 = extract.m12CD4;
            m12CD4Date = extract.m12CD4Date;
            m6CD4 = extract.m6CD4;
            m6CD4Date = extract.m6CD4Date;
            Emr = extract.Emr;
            Project = extract.Project;
            Date_Created = extract.Date_Created;
            Date_Last_Modified = extract.Date_Last_Modified;
            PatientUUID = extract.PatientUUID;

        }

        public IEnumerable<ClientPatientBaselinesExtractDTO> GeneratePatientBaselinesExtractDtOs(IEnumerable<ClientPatientBaselinesExtract> extracts)
        {
            var baselinesExtractDtos = new List<ClientPatientBaselinesExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                baselinesExtractDtos.Add(new ClientPatientBaselinesExtractDTO(e));
            }
            return baselinesExtractDtos;
        }
    }
}
