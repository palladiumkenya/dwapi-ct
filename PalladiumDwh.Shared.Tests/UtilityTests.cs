using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;

namespace PalladiumDwh.Shared.Tests
{
    [TestFixture]
    public class UtilityTests
    {
        private List<TestFacility> _facilities;

        [SetUp]
        public void SetUp()
        {
            _facilities =  Builder<TestFacility>.CreateListOfSize(7).Build() as List<TestFacility>;
        }

        [TestCase(2,4)]
        [TestCase(3, 3)]
        [TestCase(4, 2)]
        [TestCase(7, 1)]
        public void should_split_collection(int input, int expected)
        {

            var chuncks = _facilities.Split(input).ToList();

            Assert.That(chuncks.Count(), Is.EqualTo(expected));

            Console.WriteLine($"Split into Chuncks of {input} ");
            Console.WriteLine("______________________________________");
            int n=0;
            int i = 0;
            foreach (var c in chuncks)
            {
                n++; 
                Console.WriteLine($"Chucnk {n}");
                foreach (var f in c)
                {
                    i++;
                    n++;
                    Console.WriteLine($"{i}. {f}");
                }
            }
            Console.WriteLine("==========================================");
            Console.WriteLine("==========================================");

        }
        [Test]
        public void should_Get_GatMessageType()
        {
            var artProfile = PatientARTProfile.Create(new Facility(), new PatientExtract());
            var name = Utility.GetMessageType(artProfile.GetType());
            string[] names = name.Split(',');

            Assert.AreEqual(names[0], "PatientARTProfile");
            Assert.AreEqual(names[1], "PalladiumDwh.Core");
            Console.WriteLine(name);
            var type = Type.GetType(name);
            Assert.That(artProfile, Is.TypeOf<PatientARTProfile>());
        }
        [Test]
        public void should_Convert_Message()
        {
            var artProfile = PatientARTProfile.Create(new Facility(), new PatientExtract());
            var name = Utility.GetMessageType(artProfile.GetType());
            string[] names = name.Split(',');

            Assert.AreEqual(names[0], "PatientARTProfile");
            Assert.AreEqual(names[1], "PalladiumDwh.Core");
            Console.WriteLine(name);
            var type = Type.GetType(name);
            Assert.That(artProfile, Is.TypeOf<PatientARTProfile>());
        }
    }


    class TestFacility : Entity
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public TestFacility()
        {
        }

        public TestFacility(string name, int number)
        {
            Name = name;
            Number = number;
        }

        public override string ToString()
        {
            return $"{Name} ({Number})";
        }
    }
}
