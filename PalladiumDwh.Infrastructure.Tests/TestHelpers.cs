using System.Collections.Generic;
using System.Data.Entity;
using FizzWare.NBuilder;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Infrastructure.Tests
{
    public static class TestHelpers
    {
        public static void CreateTestData<T>(DbContext context, IEnumerable<T> entities) where T : Entity
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
        public static IEnumerable<T> GetTestData<T>(int count) where T : Entity
        {
            return Builder<T>.CreateListOfSize(count).Build();
        }
    }
}