// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using PalladiumDwh.Wapi.Repository;
using PalladiumDwh.Wapi.Service;

namespace PalladiumDwh.Wapi.DependencyResolution {
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<IPatientExtractRepository>().Use<PatientExtractRepository>();
            For<IPatientExtactService>().Use<PatientExtractService>();
            For<IPatientArtExtractRepository>().Use<PatientArtExtractRepository>();
            For<IPatientArtExtractService>().Use<PatientArtExtractService>();
            For<IPatientBaseLineRepository>().Use<PatientBaseLineRepository>();
            For<IPatientBaselineService>().Use<PatientBaselineService>();
            For<IPatientLabRepository>().Use<PatientLabRepository>();
            For<IPatientLabService>().Use<PatientLabService>();
            For<IPatientPharmacyRepository>().Use<PatientPharmacyRepository>();
            For<IPatientPharmacyService>().Use<PatientPharmacyService>();
            For<IPatientVisitRepository>().Use<PatientVisitRepository>();
            For<IPatientVisitService>().Use<PatientVisitService>();
            For<IPatientStatusRepository>().Use<PatientStatusRepository>();
            For<IPatientStatusService>().Use<PatientStatusService>();
        }

        #endregion
    }
}