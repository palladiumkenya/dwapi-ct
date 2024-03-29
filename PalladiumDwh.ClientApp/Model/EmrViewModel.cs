﻿using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Model
{
    public class EmrViewModel
    {
        public string Project { get; set; }
        public string EMR { get; set; }
        public EMR ActivEmr { get; set; }
        public string Version { get; set; }
        public string Key { get; set; }
        public bool IsDefault { get; set; }
        public Guid Id { get; set; }
        public List<ExtractSetting> ExtractSettings { get; set; }=new List<ExtractSetting>();

        public EmrViewModel()
        {
        }

        public EmrViewModel(Project project, EMR emr)
        {
            ActivEmr = emr;
            Project = project.Name;
            EMR = emr.Name;
            Version = emr.Version;
            Key = emr.ConnectionKey;
            IsDefault = emr.IsDefault;
            Id = emr.Id;
            ExtractSettings.AddRange(emr.ExtractSettings);            
        }

        public EMR GetEmr()
        {
            return new EMR();
        }
        public static EmrViewModel Create(Project project)
        {
            return new EmrViewModel(project, project.GetDefaultEmr());
        }
        public static List<EmrViewModel> CreateList(Project project)
        {
            var list = new List<EmrViewModel>();

            foreach (var emr in project.Emrs)
            {
                list.Add(new EmrViewModel(project,emr));
            }
            
            return list;
        }       
    }
}