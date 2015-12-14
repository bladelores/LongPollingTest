using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modem.NewAmt;
using System.Threading.Tasks;
using LongPollingTest.Connections;

namespace LongPollingTest.Controllers
{
    public class HomeController : Controller
    {
        public async Task<JsonResult> GetData(long wellboreId, DateTime startTime, DateTime? endTime, List<Modem.NewAmt.Data.Parameter> parameters)
        {
            Response.BufferOutput = false;

            IRealtimeConnection testConnection;
            testConnection = new TestConnection();

            using (var store = new Modem.NewAmt.Store())
            {
                var data = store.GetData(parameters, new Modem.NewAmt.Data.Wellbore { Id = wellboreId }, startTime, (DateTime)endTime);

                Response.Write(data);
                Response.Flush();
            }

            if (endTime == null)
                return Json(new { success = true });
            //testConnection.ConfigureConnection(wellboreId, parameters);
            while (true)
            {
                var newData = await testConnection.GetNewData();
                if (newData == null) break;

                Response.Write(newData);
                Response.Flush();
            }
            Response.End();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetParameters()
        {
            using (var store = new Modem.NewAmt.Store())
                return Json(store.Parameters.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWellbores(DateTime startTime, DateTime endTime)
        {
            using (var store = new Modem.NewAmt.Store())
            {
                var correctIntervals = new List<Modem.NewAmt.Data.ContinuousInterval>();
                foreach (var wellbore in store.Wellbores)
                {
                    var interval = store.ContinuousIntervals.Where(x => x.WellboreId == wellbore.Id).SingleOrDefault();
                    if (interval != null && endTime >= interval.StartTime && startTime <= interval.FinishTime)
                        correctIntervals.Add(interval);
                }
                Modem.NewAmt.Data.WellboreState z = new Modem.NewAmt.Data.WellboreState();
                return Json(correctIntervals.Select(x => new
                {
                    wellboreId = x.WellboreId,
                    wellboreStateId = store.WellboreStates.Any(y => y.WellboreId == x.WellboreId) 
                                      ? (long?)store.WellboreStates.Where(y => y.WellboreId == x.WellboreId).FirstOrDefault().StateId 
                                      : null,
                    startTime = x.StartTime,
                    endTime = x.FinishTime
                })
                    .ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetWellboreStates()
        {
            using (var store = new Modem.NewAmt.Store())
                return Json(store.WellboreStates.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnits()
        {
            using (var store = new Modem.NewAmt.Store())
                return Json(store.Units.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}