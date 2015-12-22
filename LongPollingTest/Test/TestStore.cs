using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modem.Amt.Export;
using Modem.Amt.Export.Data;
using NUnit.Framework;

namespace LongPollingTest.Test
{
    [TestFixture]
    public class TestStore
    {
        [Test]
        public void GetDataTest()
        {
            using (var store = new Store())
            {
                var data = store.GetData(store.Parameters.Where(x => x.Code == "GK").ToList(), new Wellbore { Id = 16314912 }, new DateTime(2013, 3, 27, 1, 34, 10), new DateTime(2013, 3, 27, 1, 34, 48));
                Assert.AreEqual(39, data.Count);
            }
        }

        [Test]
        public void WellboresTest()
        {
            using (var store = new Store())
                Assert.AreEqual("Сервер_ORACLE", store.Wellbores.Where(x => x.Id == 0).SingleOrDefault().Name);
        }

        [Test]
        public void WellboresStatesTest()
        {
            using (var store = new Store())
                Assert.AreEqual(6, store.WellboreStates.Where(x => x.Id == 38).SingleOrDefault().StateId);
        }

        [Test]
        public void ParametersTest()
        {
            using (var store = new Store())
                Assert.AreEqual("Дата начала капитального ремонта скважины", store.Parameters.Where(x => x.Id == 596).SingleOrDefault().Name);
        }

        [Test]
        public void UnitsTest()
        {
            using (var store = new Store())
                Assert.AreEqual(0.000277778, store.Units.Where(x => x.Id == 30).SingleOrDefault().Multiplier);
        }
    }
}