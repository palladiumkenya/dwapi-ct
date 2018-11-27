using NUnit.Framework;
using PalladiumDwh.DWapi.Models;

namespace PalladiumDwh.DWapi.Tests.Model
{
    [TestFixture]
    public class SubscriberTests
    {
        [Test]
        public void should_verify()
        {
            var invalidSubscriber = new Subscriber("", "");
            Assert.False(invalidSubscriber.Verify());

            var validSubscriber = new Subscriber("AMRS", "6d7c7224-m26b-11a8-8un2-f2801f1b9fd1");
            Assert.True(validSubscriber.Verify());
        }

    }
}