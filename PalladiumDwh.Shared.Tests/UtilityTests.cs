﻿using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profiles;

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
        public void should_Get_MessageType()
        {
            var artProfile = PatientARTProfile.Create(new Facility(), new PatientExtract());
            var name = Utility.GetMessageType(artProfile.GetType());
            string[] names = name.Split(',');

            Assert.That(names[0],Does.EndWith("PatientARTProfile"));
            Assert.AreEqual(names[1], " PalladiumDwh.Shared");
            
            Type type = Type.GetType(name);
            Assert.AreEqual(typeof(PatientARTProfile), type);
            Console.WriteLine(name);
        }
        [Test]
        public void should_Convert_Using_MessageType()
        {
            var artProfile = PatientARTProfile.Create(new Facility(), new PatientExtract());
            var name = Utility.GetMessageType(artProfile.GetType());

            var jsonProfile = JsonConvert.SerializeObject(artProfile);
            Console.WriteLine(jsonProfile);
            var type = Type.GetType(name);

            var fromJson = JsonConvert.DeserializeObject(jsonProfile, type);

            Assert.That(fromJson,Is.TypeOf<PatientARTProfile>());
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
