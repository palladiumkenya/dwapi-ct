using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class ImportManifest
    {
        public string Manifest { get; set; }
        public List<string> Profiles { get; set; }=new List<string>();


        public ImportManifest()
        {
        }
        private ImportManifest(string manifest)
        {
            Manifest = manifest.Trim();
        }


        private ImportManifest(string manifest, List<string> profiles)
            : this(manifest)
        {
            Profiles = profiles;
        }

        public static ImportManifest Create(string directory)
        {
            directory = directory.HasToEndsWith(@"\");

            var manifest = File.ReadAllText($@"{directory}dwapi.manifest");
            var profiles = Directory.GetFiles(directory, "*.dwh").ToList();

            return new ImportManifest(manifest, profiles);
        }

        public static ImportManifest CreateSite(string directory)
        {
            directory = directory.HasToEndsWith(@"\");

            var manifest = File.ReadAllText($@"{directory}dwapi.manifest");
            //var profiles = Directory.GetFiles(directory, "*.dwh").ToList();

            return new ImportManifest(manifest);
        }

        public override string ToString()
        {
            return $"Site(s):{Manifest} Profiles:{Profiles.Count}";
        }
    }
}
