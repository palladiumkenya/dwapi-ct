using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;

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
        public void should_convert()
        {
            var test=new TestFacility("Joy Hospital",5);
            Console.WriteLine(test.ToString());
            Console.WriteLine("==========================================");

            var liveMessage=new LiveMessage(test,test.GetType().ToString());
            Assert.IsNotNull(liveMessage);
            string jliveMessage = JsonConvert.SerializeObject(liveMessage);

            Console.WriteLine(jliveMessage);
            Console.WriteLine("==========================================");
            var objs = JsonConvert.DeserializeObject<LiveMessage>(jliveMessage);

            var readObj = objs.MessageItem;
            var converted= Convert.ChangeType(readObj, typeof(TestFacility));

            Assert.That(converted, Is.TypeOf<TestFacility>());
            Console.WriteLine(converted);
            

            
            //var readNew= Convert.ChangeType(objs.MessageItem, typeof(Type.GetType(liveMessage.MessageType)));
            /*
            Assert.IsNotNull(liveMessage);
            Console.WriteLine(liveMessage.MessageItem.ToString());
            Console.WriteLine(liveMessage.MessageType);

            var type = Type.GetType(liveMessage.MessageType);
            
            var pObject = liveMessage.MessageItem;
            */
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
