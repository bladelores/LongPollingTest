using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modem.Amt.Export.Data;
using NUnit.Framework;

namespace Modem.Amt.Export
{
    [TestFixture]
    public class TestStore
    {
        [Test]
        public void GetDataTest()
        {
            Store store = new Store();

            var r = store.Parameters;
            var r1 = store.GetData(r.Where(x => x.Code == "GK").ToList(), new Wellbore { Id = 16314912 }, new DateTime(2013, 3, 27, 1, 34, 10), new DateTime(2013, 3, 27, 1, 34, 48));
            Assert.AreEqual(39, r1.Count);
        }

        [Test]
        public void WellboresTest()
        {
            Store store = new Store();

            var wellbores = store.Wellbores;
            Assert.AreEqual(17, wellbores.Count());
            Assert.AreEqual("Сервер_ORACLE", wellbores.Where(x => x.Id == 0).SingleOrDefault().Name);
        }
    }
}
